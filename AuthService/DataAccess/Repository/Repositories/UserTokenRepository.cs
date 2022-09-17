using AuthService.Data.Entities;
using GenericRepositoryDll.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Repository.Repositories;
public class UserTokenRepository : Repository<UserTokenModel>, IUserTokenRepository
{
  public UserTokenRepository(DbContext context) : base(context)
  {
  }
}

