using AuthService.AppConstants;
using AuthService.Configurations.AppSettings;
using AuthService.Dtos.Sms;
using AuthService.Dtos.User;
using AuthService.Entities;
using AuthService.Interfaces;
using AuthService.Utils;
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
  public AuthService(IOptions<AppSetting> appSetting , IUserService userService , ISmsService smsService,
    IJwtTokenTools jwtTokenTools)
  {
    _userService = userService;
    _appSetting = appSetting.Value;
    _jwtTokenTools = jwtTokenTools;
    _smsService = smsService;
  }
  public async Task<ReturnModel<string>> SignIn(SignInDto signInDto)
  {
    ReturnModel<string> result = new();
    var getUser =await _userService.GetByCellPhone(signInDto.CellPhone);

    UserBriefDto existingUser = getUser.Data;
    if (existingUser is null)
    {
      result.CreateNotFoundModel(message: AppMessages.PleaseSignUp);
      return result;
    }

    string optCode = await SendOptSms(existingUser);

    if(optCode is not null)
    await _userService.UpdateLastAuthCode(existingUser.Id,optCode);

    result.CreateSuccessModel(optCode);
    return result;
  }

  public async Task<ReturnModel<bool>> ValidateOptCode(string optCode, string cellPhone)
  {
    ReturnModel<bool> result = new();
    var getUser = await _userService.GetByCellPhone(cellPhone);

    UserBriefDto existingUser = getUser.Data;
    if (existingUser is null)
    {
      result.CreateNotFoundModel(message: AppMessages.PleaseSignUp);
      return result;
    }



    result.CreateSuccessModel(true);
    return result;
  }
  public async Task<ReturnModel<string>> SignUp(SignUpDto signUpModel)
  {
    ReturnModel<string> result = new();
    var getUser = await _userService.GetByCellPhone(signUpModel.CellPhone);

    UserBriefDto existingUser = getUser.Data;
    if (existingUser is not null)
    {
      result.CreateBadRequestModel(message: AppMessages.PleaseSignIn);
      return result;
    }

    UserModel userModel = new UserModel();
    userModel.CreateBasicUser(signUpModel);

    var createUser = await _userService.Create(userModel);
    if(createUser.HttpStatusCode is not HttpStatusCode.OK || createUser.Data is null)
    {
      result.CreateServerErrorModel();
      return result;
    }

    UserBriefDto userBrief = new (signUpModel, (long)createUser.Data);
    var token = _jwtTokenTools.GenerateToken(userBrief);

    result.CreateSuccessModel(data : token , title : "Token");
    return result;
  }

  private async Task<string> SendOptSms(UserBriefDto userBrief )
  {
    string code = new Random().Next(4).ToString();


    SendOptSmsInputDto sendOptSmsInput = new(code, SmsProvidersId.KavehNgar, userBrief);
    var sendSms = await _smsService.SendOptSms(sendOptSmsInput);

    if (sendSms.HttpStatusCode is HttpStatusCode.OK)
      return code;
    else
      return null;

  }

}

