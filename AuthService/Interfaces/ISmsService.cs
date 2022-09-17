using AuthService.Dtos.Sms;
using ErrorHandlingDll.ReturnTypes;

namespace AuthService.Interfaces;
public interface ISmsService
{
  Task<ReturnModel<long?>> SendOptSmsAsync(SendOptSmsInputDto smsInput);
  Task<ReturnModel<long?>> SendSmsAsync(SendSmsInputDto smsInput);
}

