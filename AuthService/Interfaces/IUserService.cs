using AuthService.Dtos.User;
using AuthService.Entities;
using ErrorHandlingDll.ReturnTypes;

namespace AuthService.Interfaces;
public interface IUserService
{
  Task<ReturnModel<long?>> CreateAsync(UserModel userModel);
  Task<ReturnModel<long?>> UpdateAsync(UserModel userModel);
  Task<ReturnModel<UserModel>> GetAsync(long id);
  Task<ReturnModel<UserBriefDto>> GetByCellPhoneAsync(string cellPhone);
  Task<ReturnModel<List<UserModel>>> GetListAsync(short count = 10, short offset = 0);
  Task<ReturnModel<UserCodeDto>> GetLastAuthCodeAsync(long userId);
  Task<ReturnModel<long?>> UpdateLastAuthCodeAsync(long userId, string code);
}

