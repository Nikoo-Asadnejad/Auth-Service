using AuthService.Dtos.User;
using ErrorHandlingDll.ReturnTypes;

namespace AuthService.Interfaces;

public interface IAuthService
{
  Task<ReturnModel<string>> SignUp(UserBriefDto briefUser);
  Task<ReturnModel<string>> SignIn(UserCellPhoneDto briefUser);
}

