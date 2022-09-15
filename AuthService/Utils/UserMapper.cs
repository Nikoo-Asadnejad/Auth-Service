using AuthService.Dtos.User;
using AuthService.Entities;

namespace AuthService.Utils;
public static class UserMapper
{
  public static UserModel CreateBasicUser(this UserModel userModel, UserBriefDto userBrief)
  {
    userModel.FirstName = userBrief.FirstName;
    userModel.LastName = userBrief.LastName;
    userModel.Email = userBrief.Email;
    userModel.CellPhone = userBrief.CellPhone;
    userModel.Role = userBrief.Role;
    return userModel;
  }


}

