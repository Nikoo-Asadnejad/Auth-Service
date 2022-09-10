using AuthService.Dtos.Sms;
using ErrorHandlingDll.ReturnTypes;

namespace AuthService.Interfaces;
public interface ISmsService
{
  Task<ReturnModel<long>> SendOptSms(SendOptSmsInputDto smsInput);
  Task<ReturnModel<long>> SendSms(SendSmsInputDto smsInput);
}

