using AuthService.Dtos.User;
using AuthService.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthService.Utils.Attributes
{
  public class AuthorizationActionFilter : IAuthorizationFilter
  {
    private readonly IJwtTokenTools _jwtTools;
    private readonly string[] _roles;

    public AuthorizationActionFilter(IJwtTokenTools jwtTokenTools , string[] roles)
    {
      _jwtTools = jwtTokenTools;
      _roles = roles;
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

      if (_roles is not null && _roles.Count() > 0 && !_roles.Contains(user?.Role))
        UnAuthorize(context.HttpContext);

    }
    private void UnAuthorize(HttpContext httpContext)
    {
      //add return model to respone
      httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }
  }
}
