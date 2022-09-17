using AuthService.Entities;

namespace AuthService.Utils.Mappers;
public static class UserTokenMapper
{
  public static UserTokenModel CreateBasicUserToken(this UserTokenModel userTokenModel, long userId, string token)
  {
    userTokenModel.UserId = userId;
    userTokenModel.Token = token;
    return userTokenModel;
  }
}

