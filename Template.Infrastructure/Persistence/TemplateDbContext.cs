
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence;

public class TemplateDbContext(DbContextOptions<TemplateDbContext> options) : IdentityDbContext<User>(options)
{
    //internal DbSet<EntityType> table_name {get; set;}
    internal DbSet<Complaint> Complaints { get; set; }
    internal DbSet<ComplaintFile> ComplaintFiles { get; set; }
    internal DbSet<GovernmentalEntity> GovernmentalEntities { get; set; }
    internal DbSet<OTP> OTPs { get; set; }
    internal DbSet<History> Histories { get; set; }
    internal DbSet<Note> Notes { get; set; }
    internal DbSet<Device> Devices { get; set; }
    internal DbSet<Notification> Notifications { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //relationships between the tables

        // GovernmentalEntity → Employees (Users)
        modelBuilder.Entity<GovernmentalEntity>()
            .HasMany(ge => ge.Employees)
            .WithOne(u => u.GovernmentalEntity)
            .HasForeignKey(u => u.GovernmentalEntityId)
            .OnDelete(DeleteBehavior.NoAction);

        // GovernmentalEntity → Complaints
        modelBuilder.Entity<GovernmentalEntity>()
            .HasMany(ge => ge.Complaints)
            .WithOne(c => c.GovernmentalEntity)
            .HasForeignKey(c => c.GovernmentalEntityId)
            .OnDelete(DeleteBehavior.NoAction);

        // Complaint → User
        modelBuilder.Entity<User>()
           .HasMany(u => u.Complaints)
           .WithOne(c => c.User)
           .HasForeignKey(c => c.UserId)
           .OnDelete(DeleteBehavior.NoAction);

        // Complaint → ComplaintFiles
        modelBuilder.Entity<Complaint>()
            .HasMany(c => c.ComplaintFiles)
            .WithOne(cf => cf.Complaint)
            .HasForeignKey(cf => cf.ComplaintId)
            .OnDelete(DeleteBehavior.NoAction);

        // Complaint → Notes
        modelBuilder.Entity<Complaint>()
            .HasMany(c => c.Notes)
            .WithOne(n => n.Complaint)
            .HasForeignKey(n => n.ComplaintId)
            .OnDelete(DeleteBehavior.NoAction);

        // Complaint → Histories
        modelBuilder.Entity<Complaint>()
            .HasMany(c => c.Histories)
            .WithOne(h => h.Complaint)
            .HasForeignKey(h => h.ComplaintId)
            .OnDelete(DeleteBehavior.NoAction);

        // User → Devices
        modelBuilder.Entity<User>()
            .HasMany(u => u.Devices)
            .WithOne(d => d.User)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        // Device → Notifications
        modelBuilder.Entity<Device>()
            .HasMany(d => d.Notifications)
            .WithOne(n => n.Device)
            .HasForeignKey(n => n.DeviceId)
            .OnDelete(DeleteBehavior.NoAction);

        // RowVersion
        modelBuilder.Entity<Complaint>()
            .Property(c => c.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}
