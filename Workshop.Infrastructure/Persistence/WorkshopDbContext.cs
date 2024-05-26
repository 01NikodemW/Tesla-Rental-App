using Microsoft.EntityFrameworkCore;
using Workshop.Domain.Entities;

namespace Workshop.Infrastructure.Persistence;

internal class WorkshopDbContext(DbContextOptions<WorkshopDbContext> options) : DbContext(options)
{
    internal DbSet<Car> Cars { get; set; }
}