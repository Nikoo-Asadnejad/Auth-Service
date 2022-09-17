using AuthService.AppConstants;
using AuthService.Configurations.AppSettings;
using AuthService.Dtos.Sms;
using AuthService.Dtos.User;
using AuthService.Entities;
using AuthService.Interfaces;
using AuthService.Utils;
using AuthService.Utils.Mappers;
using ErrorHandlingDll.ReturnTypes;
using GenericRepositoryDll.Repository.GenericRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using static SmsService.Percistance.BaseData;

namespace AuthService.Services;
public class AuthService : IAuthService
{
  private readonly AppSetting _appSetting;
  private readonly IUserService _userService;
  private readonly ISmsService _smsService;
  private readonly IJwtTokenTools _jwtTokenTools;
  private readonly IOptCodeService _otpCodeService;
  public AuthService(IOptions<AppSetting> appSetting, IUserService userService, ISmsService smsService,
    IJwtTokenTools jwtTokenTools , IOptCodeService optCodeService)
  {
    _userService = userService;
    _appSetting = appSetting.Value;
    _jwtTokenTools = jwtTokenTools;
    _smsService = smsService;
    _otpCodeService = optCodeService;
  }

  public async Task<ReturnModel<string>> SignIn(SignInDto signInDto)
  {
    ReturnModel<string> result = new();
    var getUser = await _userService.GetByCellPhoneAsync(signInDto.CellPhone);

    UserBriefDto existingUser = getUser.Data;
    if (existingUser is null)
    {
      result.CreateNotFoundModel(message: AppMessages.PleaseSignUp);
      return result;
    }

    string optCode = await SendOptSms(existingUser);

    if (optCode is not null)
      await _userService.UpdateLastAuthCodeAsync(existingUser.Id, optCode);

    result.CreateSuccessModel(optCode);
    return result;
  }
  public async Task<ReturnModel<long>> SignUp(SignUpDto signUpModel)
  {
    ReturnModel<long> result = new();

    var getUser = await _userService.GetByCellPhoneAsync(signUpModel.CellPhone);
    UserBriefDto existingUser = getUser.Data;
    if (existingUser is not null)
    {
      result.CreateBadRequestModel(message: AppMessages.PleaseSignIn);
      return result;
    }

    
    UserModel userModel = new UserModel();
    userModel.CreateBasicUser(signUpModel);
    var createUser = await _userService.CreateAsync(userModel);
    if (createUser.HttpStatusCode is not HttpStatusCode.OK || createUser.Data is null)
    {
      result.CreateServerErrorModel();
      return result;
    }
    long userId = createUser.Data.Value;

    UserBriefDto userBrief = new (signUpModel, userId);
    string optCode = await SendOptSms(userBrief);
    if (optCode is not null)
      await _userService.UpdateLastAuthCodeAsync(userId, optCode);

    result.CreateSuccessModel(data:userId , title: "User Id");
    return result;
  }
  public async Task<ReturnModel<string>> GenerateToken(string optCode, string cellPhone)
  {
    ReturnModel<string> result = new();

    var getUser = await _userService.GetByCellPhoneAsync(cellPhone);

    UserBriefDto existingUser = getUser.Data;
    if (existingUser is null)
    {
      result.CreateNotFoundModel(message: AppMessages.PleaseSignUp);
      return result;
    }

    bool isOptCodeValid = await ValidateOptCodeAsync(optCode, existingUser.Id);
    if (!isOptCodeValid )
    {
      result.CreateUnAuthorizedModel(message: AppMessages.InvalidOptCode);
      return result;
    }

    var token = _jwtTokenTools.GenerateToken(existingUser);

    result.CreateSuccessModel(token, "Token");
    return result;

  }
  private async Task<bool> ValidateOptCodeAsync(string optCode, long userId)
  {
   
    var getLastCode = await _userService.GetLastAuthCodeAsync(userId);
    string lastOptCode = getLastCode.Data.Code;

    if (getLastCode.HttpStatusCode == HttpStatusCode.NotFound
        || string.IsNullOrEmpty(lastOptCode)
        || string.IsNullOrWhiteSpace(lastOptCode)
        || lastOptCode != optCode)
      return false;

    return true;
  }
  private async Task<string> SendOptSms(UserBriefDto userBrief)
  {
    string code = await CreateOptCodeAsync(userBrief.Id);
    if (code is null)
      return null;

    //after sending make opt code sent
    SendOptSmsInputDto sendOptSmsInput = new(code, SmsProvidersId.KavehNgar, userBrief);
    var sendSms = await _smsService.SendOptSmsAsync(sendOptSmsInput);

    if (sendSms.HttpStatusCode is HttpStatusCode.OK && sendSms.Data is not null)
      return code;
    else
      return null;

  }
  private async Task<string> CreateOptCodeAsync(long userId)
  {
    OptCodeModel codeModel = new ();
    codeModel.CreateBasicModelWithRandomCode(userId);

    var createOptCode = await _otpCodeService.CreateAsync(codeModel);
    if (createOptCode.HttpStatusCode is HttpStatusCode.OK && createOptCode.Data is not null)
      return codeModel.Code;

    return null;
  }
}

