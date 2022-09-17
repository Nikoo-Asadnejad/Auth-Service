namespace AuthService.DataAccess.Repository.Interfaces;
public interface IUnitOfWork
{
  IUserRepository UserRepository { get; }
  IOptCodeRepository OptCodeRepository { get; }
  IUserTokenRepository UserTokenRepository { get; }
}

