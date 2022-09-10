
using AuthService.Dtos.User;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Dtos.Sms;
public record SendSmsInputDto([Required] int TypeId, [Required] string Content, [Required] int ProviderId,
  [Required] UserBriefDto User);



