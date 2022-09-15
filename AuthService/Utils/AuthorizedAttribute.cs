using AuthService.Dtos.User;
using AuthService.Interfaces;
using ErrorHandlingDll.ReturnTypes;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace AuthService.Utils
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public class AuthorizedAttribute : Attribute, IAuthorizationFilter
  {
    private readonly IJwtTokenTools _jwtTools;
    public AuthorizedAttribute(IJwtTokenTools jwtTokenTools)
    {
      _jwtTools = jwtTokenTools;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
      var token = context.HttpContext.Request.Headers["Authorization"];
      if (string.IsNullOrEmpty(token))
        UnAuthorize(context.HttpContext);

      UserBriefDto user;
      var isTokenValid  = _jwtTools.ValidateToken(token, out user);

      if(!isTokenValid || user is null)
        UnAuthorize(context.HttpContext);

        // attach user to context on successful jwt validation
        context.HttpContext.Items["User"] = user;

    }
    private void UnAuthorize(HttpContext httpContext)
    {
      //add return model to respone
      httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }
  }
}
