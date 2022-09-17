using AuthService.Entities;
using GenericRepositoryDll.Repository.GenericRepository;

namespace AuthService.DataAccess.Repository
{
  public interface IUserRepository : IRepository<UserModel>
  {
  }
}
