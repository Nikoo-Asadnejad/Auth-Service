using AuthService.Dtos.User;
using AuthService.Interfaces;
using ErrorHandlingDll.ReturnTypes;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using static SmsService.Percistance.BaseData;

namespace AuthService.Utils
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public class AuthorizedAttribute : Attribute, IAuthorizationFilter
  {
    private string _role;

    private readonly IJwtTokenTools _jwtTools;

    private AuthorizedAttribute(IJwtTokenTools jwtTokenTools)
    {
      _jwtTools = jwtTokenTools;
    }
    public AuthorizedAttribute(string role = null)
    {
      _role = role;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
      var token = context.HttpContext.Request.Headers["Authorization"];
      if (string.IsNullOrEmpty(token))
        UnAuthorize(context.HttpContext);

      UserBriefDto user;
      var isTokenValid = _jwtTools.ValidateToken(token, out user);

      if (!isTokenValid || user is null)
        UnAuthorize(context.HttpContext);

      context.HttpContext.Items["User"] = user;

      if (_role is not null && _role != user?.Role)
        UnAuthorize(context.HttpContext);

    }
    private void UnAuthorize(HttpContext httpContext)
    {
      //add return model to respone
      httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }
  }
}
