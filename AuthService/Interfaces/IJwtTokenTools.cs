using AuthService.Dtos.User;

namespace AuthService.Interfaces;
  public interface IJwtTokenTools
  {
     string GenerateToken(UserBriefDto userModel);
     bool ValidateToken(string token, out UserBriefDto user);
  }

