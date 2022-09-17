using AuthService.Data.Entities;
using AuthService.Interfaces;
using ErrorHandlingDll.ReturnTypes;

namespace AuthService.Services;
public class UserTokenService : IUserTokenService
{
  public Task<ReturnModel<long>> CreateAsync(UserTokenModel userTokenModel)
  {
    throw new NotImplementedException();
  }
}

