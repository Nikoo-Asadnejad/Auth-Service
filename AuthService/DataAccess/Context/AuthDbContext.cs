using AuthService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data
{
  public class AuthDbContext : DbContext
  {
    public AuthDbContext(DbContextOptions options): base(options)
    {

    }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<OptCodeModel> OptCodes { get; set; }
    public DbSet<UserTokenModel> UserTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<UserModel>()
        .HasQueryFilter(modelBuilder => modelBuilder.DeleteDate == null && modelBuilder.DeleteDate == null);

      modelBuilder.Entity<OptCodeModel>()
        .HasQueryFilter(modelBuilder => modelBuilder.DeleteDate == null && modelBuilder.DeleteDate == null);

      modelBuilder.Entity<UserTokenModel>()
        .HasQueryFilter(modelBuilder => modelBuilder.DeleteDate == null && modelBuilder.DeleteDate == null);
    }
  }
}
