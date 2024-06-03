using Microsoft.EntityFrameworkCore;
using Workshop.Domain.Entities;
using Workshop.Domain.Repositories;
using Workshop.Infrastructure.Persistence;

namespace Workshop.Infrastructure.Repositories;

internal class CountriesRepository(WorkshopDbContext dbContext) : ICountriesRepository
{
    public async Task<IEnumerable<Country>> GetAllCountries()
    {
        var countries = await dbContext.Countries.ToListAsync();
        return countries;
    }

    public async Task<Country?> GetCountryById(Guid id)
    {
        var country = await dbContext.Countries.FirstOrDefaultAsync(c => c.Id == id);
        return country;
    }
}