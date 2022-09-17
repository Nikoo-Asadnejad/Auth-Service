using AuthService.Data.Entities;
using ErrorHandlingDll.ReturnTypes;
namespace AuthService.Interfaces;
public interface IUserTokenService
{
  Task<ReturnModel<long>> CreateAsync(UserTokenModel userTokenModel);
}

