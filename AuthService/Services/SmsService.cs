using AuthService.Dtos.Sms;
using AuthService.Interfaces;
using ErrorHandlingDll.ReturnTypes;

namespace AuthService.Services;
public class SmsService : ISmsService
{
  public Task<ReturnModel<long>> SendOptSms(SendOptSmsInputDto smsInput)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<long>> SendSms(SendSmsInputDto smsInput)
  {
    throw new NotImplementedException();
  }
}

