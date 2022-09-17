using AuthService.Entities;
using ErrorHandlingDll.ReturnTypes;

namespace AuthService.Interfaces;

public interface IOptCodeService
{
  Task<ReturnModel<long?>> CreateAsync(OptCodeModel userModel);
  Task<ReturnModel<long?>> UpdateAsync(OptCodeModel userModel);
  Task<ReturnModel<OptCodeModel>> GetAsync(long id);
  Task<ReturnModel<OptCodeModel>> GetByCodeAsync(string code);
  Task<ReturnModel<List<OptCodeModel>>> GetListAsync(short count = 10, short ofset =0);
}


