using AuthService.Dtos.User;
using AuthService.Entities;
using static SmsService.Percistance.BaseData;

namespace AuthService.Utils;
public static class UserMapper
{
  public static UserModel CreateBasicUser(this UserModel userModel, SignUpDto userBrief)
  {
    userModel.FirstName = userBrief.FirstName;
    userModel.LastName = userBrief.LastName;
    userModel.CellPhone = userBrief.CellPhone;
    userModel.Role = UserRoles.User;
    return userModel;
  }


}

