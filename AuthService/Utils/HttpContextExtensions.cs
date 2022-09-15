using AuthService.Dtos.User;
using static SmsService.Percistance.BaseData;

namespace AuthService.Utils;
public static class HttpContextExtensions
{
  public static UserBriefDto GetUser(this HttpContext httpContext)
   => (UserBriefDto)httpContext.Items["User"];

  public static bool IsAuthorized(this HttpContext httpContext)
    => httpContext.Items["User"] is not null;

  public static bool IsUserStaff(this HttpContext httpContext)
    => httpContext.GetUser().Role is UserRoles.Admin || httpContext.GetUser().Role is UserRoles.Staff;

  public static bool IsUserAdmin(this HttpContext httpContext)
  => httpContext.GetUser().Role is UserRoles.Admin;
}

