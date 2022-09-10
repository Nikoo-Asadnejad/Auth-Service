using AuthService.Dtos.User;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Dtos.Sms;
public record SendOptSmsInputDto([Required] string Code, [Required] int ProviderId,
  [Required] UserBriefDto User);


