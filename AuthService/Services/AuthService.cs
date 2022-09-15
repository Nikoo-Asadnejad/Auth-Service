using AuthService.Configurations.AppSettings;
using AuthService.Dtos.User;
using AuthService.Entities;
using AuthService.Interfaces;
using ErrorHandlingDll.ReturnTypes;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Services;
public class AuthService : IAuthService
{
  private readonly AppSetting _appSetting;
  public AuthService(IOptions<AppSetting> appSetting)
  {
    _appSetting = appSetting.Value; 
  }
  public Task<ReturnModel<string>> SignIn(UserCellPhoneDto briefUser)
  {
    throw new NotImplementedException();
  }

  public Task<ReturnModel<string>> SignUp(UserBriefDto briefUser)
  {
    throw new NotImplementedException();
  }


}

