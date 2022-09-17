
using AuthService.DataAccess.Repository.Interfaces;
using AuthService.Entities;
using AuthService.Interfaces;
using ErrorHandlingDll.ReturnTypes;
using GenericRepositoryDll.Repository.GenericRepository;

namespace AuthService.Services;
public class UserTokenService : IUserTokenService
{
  private readonly IRepository<UserTokenModel> _userTokenRepository;

  public UserTokenService(IUnitOfWork unitOfWork)
  {
    _userTokenRepository = unitOfWork.UserTokenRepository;
  }
  public Task<ReturnModel<long>> CreateAsync(UserTokenModel userTokenModel)
  {
    throw new NotImplementedException();
  }
}

