using Microsoft.EntityFrameworkCore;
using Workshop.Domain.Constants;
using Workshop.Domain.Entities;

namespace Workshop.Infrastructure.Persistence;

public class WorkshopDbContext(DbContextOptions<WorkshopDbContext> options) : DbContext(options)
{
    internal DbSet<Car> Cars { get; set; }
    internal DbSet<User> Users { get; set; }
    internal DbSet<Country> Countries { get; set; }

    internal DbSet<Location> Locations { get; set; }
    internal DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Car>()
            .Property(c => c.Model)
            .HasConversion(
                v => v.ToString(),
                v => (TeslaModel)Enum.Parse(typeof(TeslaModel), v));

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.RentalLocation)
            .WithMany()
            .HasForeignKey(r => r.RentalLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.ReturnLocation)
            .WithMany()
            .HasForeignKey(r => r.ReturnLocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}