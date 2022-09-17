using AuthService.Dtos.User;
using ErrorHandlingDll.ReturnTypes;

namespace AuthService.Interfaces;

public interface IAuthService
{
  Task<ReturnModel<long>> SignUp(SignUpDto signUpModel );
  Task<ReturnModel<long>> SignIn(SignInDto briefUser);
  Task<ReturnModel<string>> GenerateToken(GenerateTokenDto input);
}

