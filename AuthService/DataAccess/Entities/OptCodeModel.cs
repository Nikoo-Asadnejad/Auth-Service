using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Entities;

[Table("OptCodes")]
public class OptCodeModel : BaseEntityModel
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  [Required]
  public long Id { get; set; }

  [Required]
  [MaxLength(4)]
  public string Code { get; set; }

  [Required]
  public bool IsUsed { get; set; }
  public bool? IsSent { get; set; }
  public long? ExpireDate { get; set; }

  [Required]
  public long UserId { get; set; }

  [ForeignKey("UserId")]
  public virtual UserModel User { get; set; }

  public OptCodeModel() : base()
  {

  }

}

