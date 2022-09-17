using AuthService.Dtos.Sms;
using AuthService.Dtos.User;
using ErrorHandlingDll.ReturnTypes;

namespace AuthService.Interfaces;
public interface ISmsService
{
  Task<string> SendOptSmsAsync(UserBriefDto userBrief);
}

