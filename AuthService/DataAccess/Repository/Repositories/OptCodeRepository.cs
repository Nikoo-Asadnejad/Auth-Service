using AuthService.Entities;
using GenericRepositoryDll.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DataAccess.Repository.Repositories;
public class OptCodeRepository : Repository<OptCodeModel>, IOptCodeRepository
{
  public OptCodeRepository(DbContext context) : base(context)
  {
  }
}

