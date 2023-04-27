using AuthService.Dtos.User;
using AuthService.Interfaces;
using ErrorHandlingDll.ReturnTypes;
using HttpService.Utils;
using Microsoft.AspNetCore.Mvc;
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
        UnAuthorize(context);

      UserBriefDto user;
      var isTokenValid = _jwtTools.ValidateToken(token, out user);

      if (!isTokenValid || user is null)
        UnAuthorize(context);

      context.HttpContext.Items["User"] = user;

      if (_roles is not null && _roles.Count() > 0 && !_roles.Contains(user?.Role))
        UnAuthorize(context);

    }
    private void UnAuthorize(AuthorizationFilterContext httpContext)
    =>httpContext.Result = new UnauthorizedObjectResult(new ReturnModel<string>().CreateUnAuthorizedModel());
    
  }
}
