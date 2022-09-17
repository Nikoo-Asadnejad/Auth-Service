
using AuthService.Entities;
using GenericRepositoryDll.Repository.GenericRepository;

namespace AuthService.DataAccess.Repository.Interfaces;
public interface IUnitOfWork
{
  IRepository<UserModel> UserRepository { get; }
  IRepository<OptCodeModel> OptCodeRepository { get; }
  IRepository<UserTokenModel> UserTokenRepository { get; }
}

