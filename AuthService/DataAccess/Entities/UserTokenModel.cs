using AuthService.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Data.Entities;
[Table("UserTokenModel")]
public class UserTokenModel : BaseEntityModel
{
  [Key]
  [Required]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public long Id { get; set; }
  [Required]
  public long UserId { get; set; }
  [ForeignKey("UserId")]
  public virtual UserModel User { get; set; }
  [Required]
  public string Token   { get; set; }
  public bool IsExpired { get; set; }
  public long? ExpireDate  { get; set; }
}

