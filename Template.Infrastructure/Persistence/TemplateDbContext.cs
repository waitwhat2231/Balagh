
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Entities.Notifications;

namespace Template.Infrastructure.Persistence;

public class TemplateDbContext(DbContextOptions<TemplateDbContext> options) : IdentityDbContext<User>(options)
{
    //internal DbSet<EntityType> table_name {get; set;}

    internal DbSet<Device> Devices { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<User>()
       .HasIndex(u => u.NormalizedEmail)
       .IsUnique();
        modelBuilder.Entity<User>()
      .HasMany(u => u.Devices)
      .WithOne(d => d.User)
      .HasForeignKey(d => d.UserId)
      .OnDelete(DeleteBehavior.Cascade);

        //relationships between the tables
    }
}
