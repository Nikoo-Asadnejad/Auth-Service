using AuthService.Dtos.User;
using ErrorHandlingDll.ReturnTypes;

namespace AuthService.Interfaces;

public interface IAuthService
{
  Task<ReturnModel<long>> SignUp(SignUpDto signUpModel );
  Task<ReturnModel<string>> SignIn(SignInDto briefUser);
}

