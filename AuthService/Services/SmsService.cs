using AuthService.Dtos.Sms;
using AuthService.Interfaces;
using ErrorHandlingDll.ReturnTypes;

namespace AuthService.Services;
public class SmsService : ISmsService
{
  public Task<ReturnModel<long?>> SendOptSmsAsync(SendOptSmsInputDto smsInput)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<long?>> SendSmsAsync(SendSmsInputDto smsInput)
  {
    throw new NotImplementedException();
  }
}

