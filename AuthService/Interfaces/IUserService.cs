using AuthService.Dtos.User;
using AuthService.Entities;
using ErrorHandlingDll.ReturnTypes;

namespace AuthService.Interfaces;
public interface IUserService
{
  Task<ReturnModel<long?>> Create(UserModel userModel);
  Task<ReturnModel<long?>> Update(UserModel userModel);
  Task<ReturnModel<UserModel>> Get(long id);
  Task<ReturnModel<UserBriefDto>> GetByCellPhone(string cellPhone);
  Task<ReturnModel<List<UserModel>>> GetList(short count = 10, short offset = 0);
  Task<ReturnModel<UserCodeDto>> GetLastAuthCode(long userId);
  Task<ReturnModel<long?>> UpdateLastAuthCode(long userId, string code);
}

