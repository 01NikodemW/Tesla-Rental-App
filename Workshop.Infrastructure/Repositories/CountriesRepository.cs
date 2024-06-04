using Microsoft.EntityFrameworkCore;
using Workshop.Domain.Entities;
using Workshop.Domain.Repositories;
using Workshop.Infrastructure.Persistence;

namespace Workshop.Infrastructure.Repositories;

internal class CountriesRepository(WorkshopDbContext dbContext) : ICountriesRepository
{
    public async Task<IEnumerable<Country>> GetAllCountries(CancellationToken cancellationToken)
    {
        var countries = await dbContext.Countries.ToListAsync(cancellationToken);
        return countries;
    }


    public async Task<Country?> GetCountryById(Guid id, CancellationToken cancellationToken)
    {
        var country = await dbContext.Countries.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        return country;
    }

    public bool CheckIfCountryWithProvidedIdInDb(Guid id)
    {
        return dbContext.Countries.Any(c => c.Id == id);
    }
}