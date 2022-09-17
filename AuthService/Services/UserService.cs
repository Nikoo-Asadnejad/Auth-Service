using AuthService.Dtos.User;
using AuthService.Entities;
using AuthService.Interfaces;
using ErrorHandlingDll.ReturnTypes;
using GenericRepositoryDll.Repository.GenericRepository;

namespace AuthService.Services;
public class UserService : IUserService
{
  private readonly IRepository<UserModel> _userRepository;
  public UserService(IRepository<UserModel> userRepository)
  {
    _userRepository = userRepository;
  }
  public async Task<ReturnModel<long?>> CreateAsync(UserModel userModel)
  {
    ReturnModel<long?> result = new();
    await _userRepository.AddAsync(userModel);
    await _userRepository.SaveAsync();

    result.CreateSuccessModel(data: userModel.Id, "UserId");
    return result;
  }
  public async Task<ReturnModel<UserModel>> GetAsync(long id)
  {
    ReturnModel<UserModel> result = new();

    UserModel user = await _userRepository.FindAsync(id);
    if(user is null)
    {
      result.CreateNotFoundModel();
      return result;
    }

    result.CreateSuccessModel(data: user);
    return result;
  }
  public async Task<ReturnModel<UserBriefDto>> GetByCellPhoneAsync(string cellPhone)
  {
    ReturnModel<UserBriefDto> result = new();

    UserBriefDto user = (UserBriefDto)await _userRepository.GetSingleAsync(query: u => u.CellPhone == cellPhone,
                                                                                    selector: u => new UserBriefDto(u));
    if (user is null)
    {
      result.CreateNotFoundModel();
      return result;
    }

    result.CreateSuccessModel(data: user);
    return result;
  }
  public async Task<ReturnModel<List<UserModel>>> GetListAsync(short count = 10, short offset = 0)
  {
    ReturnModel<List<UserModel>> result = new();

    List<UserModel> users =await _userRepository.GetListAsync<UserModel>(skip: count, take: offset);

    result.CreateSuccessModel(data: users);
    return result;
  }
  public async Task<ReturnModel<long?>> UpdateAsync(UserModel userModel)
  {
    ReturnModel<long?> result = new();

    await _userRepository.UpdateAsync(userModel);

    result.CreateSuccessModel(data: userModel.Id , "User Id");
    return result;
  }
  public async Task<ReturnModel<UserCodeDto>> GetLastAuthCodeAsync(long userId)
  {
    ReturnModel<UserCodeDto> result = new();

    UserCodeDto userCode = (UserCodeDto) await _userRepository.GetSingleAsync(query: u => u.Id == userId,
                                                     selector: u => new UserCodeDto(u.Id, u.LastAuthCode));
    if(userCode is null)
    {
      result.CreateNotFoundModel();
      return result;
    }

    result.CreateSuccessModel(data: userCode);
    return result;
  }
  public async Task<ReturnModel<long?>> UpdateLastAuthCodeAsync(long userId,string code)
  {
    ReturnModel<long?> result = new();

    var user = await _userRepository.FindAsync(userId);
    if(user is null)
    {
      result.CreateNotFoundModel();
      return result;
    }

    user.LastAuthCode = code;
    await _userRepository.UpdateAsync(user);
    await _userRepository.SaveAsync();

    return result;
  }

}

