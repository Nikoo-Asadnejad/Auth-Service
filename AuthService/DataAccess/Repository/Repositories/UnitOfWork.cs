
using AuthService.DataAccess.Repository.Interfaces;
using AuthService.Entities;
using GenericRepositoryDll.Repository.GenericRepository;

namespace AuthService.DataAccess.Repository.Repositories;
public class UnitOfWork : IUnitOfWork
{
  public IRepository<UserModel> UserRepository { get; private set; }

  public IRepository<OptCodeModel> OptCodeRepository { get; private set; }

  public IRepository<UserTokenModel> UserTokenRepository { get; private set; }

  public UnitOfWork(IRepository<UserModel> userRepository , IRepository<OptCodeModel> optCodeRepository,
                     IRepository<UserTokenModel> userTokenRepository)
  {
    UserRepository = userRepository;
    OptCodeRepository = optCodeRepository;
    UserTokenRepository = userTokenRepository ;
  }
}

