using AuthService.Dtos.User;
using AuthService.Interfaces;
using ErrorHandlingDll.ReturnTypes;

namespace AuthService.Services;
public class AuthService : IAuthService
{
  public Task<ReturnModel<string>> SignIn(UserCellPhoneDto briefUser)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<string>> SignUp(UserBriefDto briefUser)
  {
    throw new NotImplementedException();
  }
}

