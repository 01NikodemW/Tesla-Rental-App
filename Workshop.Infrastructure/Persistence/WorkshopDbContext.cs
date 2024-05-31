using Microsoft.EntityFrameworkCore;
using Workshop.Domain.Entities;

namespace Workshop.Infrastructure.Persistence;

internal class WorkshopDbContext(DbContextOptions<WorkshopDbContext> options) : DbContext(options)
{
    internal DbSet<Car> Cars { get; set; }
    internal DbSet<User> Users { get; set; }
    internal DbSet<Country> Countries { get; set; }

    internal DbSet<Location> Locations { get; set; }
    // public DbSet<Reservation> Reservations { get; set; } = default!;
}