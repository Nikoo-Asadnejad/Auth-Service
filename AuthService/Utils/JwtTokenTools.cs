using AuthService.AppConstants;
using AuthService.Configurations.AppSettings;
using AuthService.Dtos.User;
using AuthService.Entities;
using AuthService.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Utils;

  public  class JwtTokenTools : IJwtTokenTools
{
  private readonly AppSetting _appSetting;
  public JwtTokenTools(IOptions<AppSetting> appSetting)
  {
    _appSetting = appSetting.Value;
  }
  public string GenerateToken(UserBriefDto userModel)
  {
    string issuer = _appSetting.JWT.Issuer;
    string audience = _appSetting.JWT.Audience;
    byte[] key = Encoding.ASCII.GetBytes(_appSetting.JWT.Key);
    SigningCredentials credentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);
    ClaimsIdentity claims = GenerateClaims(userModel);

    SecurityTokenDescriptor tokenDescriptor = new()
    {
      Subject = claims,
      Issuer = issuer,
      IssuedAt = DateTime.UtcNow,
      Audience = audience,
      SigningCredentials = credentials
    };

    JwtSecurityTokenHandler tokenHandler = new();
    SecurityToken JWTtoken = tokenHandler.CreateToken(tokenDescriptor);
    string token = tokenHandler.WriteToken(JWTtoken);

    return token;
  }
  public bool ValidateToken(string token, out UserBriefDto user)
  {
    if (token is null)
    {
      user = null;
      return false;
    }
    try
    {
      JwtSecurityTokenHandler tokenHandler = new();
      byte[] key = Encoding.ASCII.GetBytes(_appSetting.JWT.Key);
      TokenValidationParameters tokenValidationParameters = new()
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = false
      };
      tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
      JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;

      user = GetUser(jwtToken);
      return true;
    }
    catch
    {
      user = null;
      return false;
    }
  }
  private  ClaimsIdentity GenerateClaims(UserBriefDto userModel)
    => new ClaimsIdentity(new[]
        {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(TokenClaims.UserId, userModel.Id.ToString()),
                new Claim(TokenClaims.Role, userModel.Role),
                new Claim(TokenClaims.FirstName, userModel.FirstName),
                new Claim(TokenClaims.LastName, userModel.LastName),
                new Claim(TokenClaims.CellPhone, userModel.CellPhone),
                new Claim(TokenClaims.Email, userModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
         });
  private UserBriefDto GetUser(JwtSecurityToken token)
  {
    long id;
    long.TryParse(token.Claims.FirstOrDefault(c => c.Type is TokenClaims.UserId).Value, out id);
    string role = token.Claims.FirstOrDefault(c => c.Type is TokenClaims.Role).Value;
    string firstName = token.Claims.FirstOrDefault(c => c.Type is TokenClaims.FirstName).Value;
    string lastName = token.Claims.FirstOrDefault(c => c.Type is TokenClaims.LastName).Value;
    string cellPhone = token.Claims.FirstOrDefault(c => c.Type is TokenClaims.CellPhone).Value;
    string email = token.Claims.FirstOrDefault(c => c.Type is TokenClaims.Email).Value;

    UserBriefDto userBrief = new(id, firstName, lastName, cellPhone, email, role);

    return userBrief;
  }
}

