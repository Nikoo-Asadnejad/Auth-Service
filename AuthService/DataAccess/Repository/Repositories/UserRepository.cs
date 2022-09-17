using AuthService.Entities;
using GenericRepositoryDll.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Repository.Repositories;

public class UserRepository : Repository<UserModel>, IUserRepository
{
  public UserRepository(DbContext context) : base(context)
  {
  }
}



