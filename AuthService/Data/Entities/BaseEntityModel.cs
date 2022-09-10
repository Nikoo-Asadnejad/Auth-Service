namespace AuthService.Entities;
public class BaseEntityModel
{
  public long CreateDate { get; set; }
  public long CreatedBy { get; set; }
  public long? EditDate { get; set; }
  public long? EditedBy { get; set; }
  public long? DeleteDate { get; set; }
  public long? DeletedBy { get; set; }

  public BaseEntityModel()
  {
    CreateDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
  }
}

