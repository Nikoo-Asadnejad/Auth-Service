using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Entities;
[Table("Users")]
public class UserModel : BaseEntityModel
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  [Required]
  public long Id { get; set; }

  [Required]
  [MaxLength(255)]
  [MinLength(2)]
  public string FirstName { get; set; }

  [Required]
  [MaxLength(255)]
  [MinLength(2)]
  public string LastName { get; set; }

  [Required]
  [StringLength(3)]
  [RegularExpression("ADM|STF|USR")]
  public string Role { get; set; }

  [Required]
  [MaxLength(11)]
  [StringLength(11)]
  public string CellPhone { get; set; }

  [EmailAddress]
  [MaxLength(50)]
  public string Email { get; set; }
  [MaxLength(50)]
  public string Ip { get; set; }
  [MaxLength(255)]
  public string UserAgent { get; set; }

  [RegularExpression("F|M")]
  [MaxLength(1)]
  public char Gender { get; set; }
  public string LastAuthCode { get; set; }
  public virtual ICollection<OptCodeModel> AuthCodes { get; set; }
  public virtual ICollection<UserTokenModel> UserTokens { get; set; }
  public UserModel() :base()
  {

  }


}


