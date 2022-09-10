using AuthService.Entities;
using ErrorHandlingDll.ReturnTypes;

namespace AuthService.Interfaces;

public interface IOptCodeService
{
  Task<ReturnModel<long?>> Create(OptCodeModel userModel);
  Task<ReturnModel<long?>> Update(OptCodeModel userModel);
  Task<ReturnModel<OptCodeModel>> Get(long id);
  Task<ReturnModel<OptCodeModel>> Get(string code);
  Task<ReturnModel<string>> GetUserLastAuthCode(long userId);
  Task<ReturnModel<List<OptCodeModel>>> GetList(short count = 10, short ofset =0);
}


