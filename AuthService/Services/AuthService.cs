using AuthService.AppConstants;
using AuthService.Configurations.AppSettings;
using AuthService.Dtos.Sms;
using AuthService.Dtos.User;
using AuthService.Entities;
using AuthService.Interfaces;
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
  public AuthService(IOptions<AppSetting> appSetting , IUserService userService , ISmsService smsService)
  {
    _userService = userService;
    _appSetting = appSetting.Value;
    _smsService = smsService;
  }
  public async Task<ReturnModel<string>> SignIn(UserCellPhoneDto userCellPhone)
  {
    ReturnModel<string> result = new();
    var getUser =await _userService.GetByCellPhone(userCellPhone.CellPhone);

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

  public Task<ReturnModel<string>> SignUp(UserBriefDto briefUser)
  {
    throw new NotImplementedException();
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

