using AuthService.AppConstants;
using AuthService.Configurations.AppSettings;
using AuthService.Dtos.Sms;
using AuthService.Dtos.User;
using AuthService.Interfaces;
using ErrorHandlingDll.ReturnTypes;
using HttpService.Interface;
using Microsoft.Extensions.Options;
using System.Net;
using static SmsService.Percistance.BaseData;

namespace AuthService.Services;
public class SmsService : ISmsService
{
  private readonly IOptCodeService _otpCodeService;
  private readonly IHttpService _httpService;
  private readonly AppSetting _appSetting;
  public SmsService(IOptCodeService optCodeService , IHttpService httpService, IOptions<AppSetting> appSetting)
  {
    _otpCodeService = optCodeService;
    _appSetting = appSetting.Value;
    _httpService = httpService;
  }

  public async Task<string> SendOptSmsAsync(UserBriefDto userBrief)
  {
    string code = await _otpCodeService.CreateRandomCodeAsync(userBrief.Id);
    if (code is null)
      return null;

    //after sending make opt code sent
    SendOptSmsInputDto sendOptSmsInput = new(code, SmsProvidersId.KavehNgar, userBrief);
    var sendSms = await SendSmsAsync(sendOptSmsInput);

    if (sendSms is not null)
      return code;
    else
      return null;

  }

  private async Task<string?> SendSmsAsync(SendOptSmsInputDto smsInput)
  {
    string url = _appSetting.Microservices.SmsService + ApiUrls.SendSms;
    var request = await _httpService.PostAsync<ReturnModel<SendSmsReturnDto>>(url,smsInput);
    if (request.HttpStatusCode is HttpStatusCode.OK && request.Data is not null)
      return request.Data.Data.SmsId;

    return null;
  }

}

