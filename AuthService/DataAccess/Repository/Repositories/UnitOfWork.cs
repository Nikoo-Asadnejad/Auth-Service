using AuthService.DataAccess.Repository.Interfaces;

namespace AuthService.DataAccess.Repository.Repositories;
public class UnitOfWork : IUnitOfWork
{
  public IUserRepository UserRepository { get; private set; }

  public IOptCodeRepository OptCodeRepository { get; private set; }

  public IUserTokenRepository UserTokenRepository { get; private set; }

  public UnitOfWork(IUserRepository userRepository , IOptCodeRepository optCodeRepository,
                     IUserTokenRepository userTokenRepository)
  {
    UserRepository = userRepository;
    OptCodeRepository = optCodeRepository;
    UserTokenRepository = userTokenRepository ;
  }
}

