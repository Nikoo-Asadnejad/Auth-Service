
using AuthService.DataAccess.Repository.Interfaces;
using AuthService.Entities;
using AuthService.Interfaces;
using AuthService.Utils.Mappers;
using ErrorHandlingDll.ReturnTypes;
using GenericRepositoryDll.Repository.GenericRepository;

namespace AuthService.Services;
public class UserTokenService : IUserTokenService
{
  private readonly IRepository<UserTokenModel> _userTokenRepository;

  public UserTokenService(IUnitOfWork unitOfWork)
  {
    _userTokenRepository = unitOfWork.UserTokenRepository;
  }
  public async Task<long> CreateAsync(long userId, string token)
  {
    UserTokenModel userTokenModel = new();
    userTokenModel.CreateBasicUserToken(userId, token);

    await _userTokenRepository.AddAsync(userTokenModel);
    await _userTokenRepository.SaveAsync();

    return userTokenModel.Id;
  }

  public async Task ExpireOldTokensAsync(long userId)
  {
    List<UserTokenModel> tokens = await _userTokenRepository
                                .GetListAsync<UserTokenModel>(query: t => t.UserId == userId
                                                                  && t.IsExpired == false);

    //it may crash due to change tracking of the repository , if so check repository dll
    tokens.ForEach(t => t.IsExpired = true);

    await _userTokenRepository.UpdateRangeAsync(tokens);
    await _userTokenRepository.SaveAsync();
  }
}

