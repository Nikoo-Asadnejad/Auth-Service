using AuthService.Dtos.User;
using AuthService.Interfaces;
using AuthService.Utils.Attributes;
using ErrorHandlingDll.ReturnTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using static SmsService.Percistance.BaseData;

namespace AuthService.Utils
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public class AuthorizedAttribute : TypeFilterAttribute
  { 
    public AuthorizedAttribute(params string[] roles ) : base(typeof(AuthorizationActionFilter))
    {
      Arguments = new object[] { roles };
    }

  }
}
