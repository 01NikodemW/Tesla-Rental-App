using Microsoft.EntityFrameworkCore;
using Workshop.Domain.Entities;
using Workshop.Domain.Repositories;
using Workshop.Infrastructure.Persistence;

namespace Workshop.Infrastructure.Repositories;

internal class LocationsRepository(WorkshopDbContext dbContext) : ILocationsRepository
{
    public async Task<IEnumerable<Location>> GetAllLocations(CancellationToken cancellationToken)
    {
        var locations = await dbContext.Locations.ToListAsync(cancellationToken);
        return locations;
    }

    public async Task<Location?> GetLocationById(Guid id, CancellationToken cancellationToken)
    {
        var location = await dbContext.Locations.FindAsync(id, cancellationToken);
        return location;
    }
}