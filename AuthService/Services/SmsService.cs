using AuthService.Dtos.Sms;
using AuthService.Dtos.User;
using AuthService.Interfaces;
using ErrorHandlingDll.ReturnTypes;
using System.Net;
using static SmsService.Percistance.BaseData;

namespace AuthService.Services;
public class SmsService : ISmsService
{
  private readonly IOptCodeService _otpCodeService;
  public SmsService(IOptCodeService optCodeService)
  {
    _otpCodeService = optCodeService;
  }

  public async Task<string> SendOptSmsAsync(UserBriefDto userBrief)
  {
    string code = await _otpCodeService.CreateRandomCodeAsync(userBrief.Id);
    if (code is null)
      return null;

    //after sending make opt code sent
    SendOptSmsInputDto sendOptSmsInput = new(code, SmsProvidersId.KavehNgar, userBrief);
    var sendSms = await SendSmsAsync(sendOptSmsInput);

    if (sendSms.HttpStatusCode is HttpStatusCode.OK && sendSms.Data is not null)
      return code;
    else
      return null;

  }

  private Task<ReturnModel<long?>> SendSmsAsync(SendOptSmsInputDto smsInput)
  {
    throw new NotImplementedException();
  }

}

