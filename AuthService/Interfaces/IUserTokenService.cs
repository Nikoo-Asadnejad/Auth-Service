using AuthService.Entities;
using ErrorHandlingDll.ReturnTypes;
namespace AuthService.Interfaces;
public interface IUserTokenService
{
  Task<long> CreateAsync(long userId , string token);
  Task ExpireOldTokensAsync(long userId);
}

