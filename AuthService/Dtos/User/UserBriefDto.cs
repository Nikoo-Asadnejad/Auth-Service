
using AuthService.Entities;

namespace AuthService.Dtos.User;
public class UserBriefDto
{

  public long Id { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string CellPhone { get; set; }
  public string Email { get; set; }
  public string Role { get; set; }

  public UserBriefDto()
  {

  }

  public UserBriefDto(long id, string firstName, string lastName,
    string cellPhone, string email, string role)
  {
    Id = id;
    FirstName = firstName;
    LastName = lastName;
    CellPhone = cellPhone;
    Email = email;
    Role = role;
  }

  public UserBriefDto(UserModel userModel)
  {
    Id = userModel.Id;
    FirstName = userModel.FirstName;
    LastName = userModel.LastName;
    CellPhone = userModel.CellPhone;
    Email = userModel.Email;
    Role = userModel.Role;
  }
}


