using Workshop.Domain.Entities;

namespace Workshop.Domain.Repositories;

public interface ICountriesRepository
{
    Task<IEnumerable<Country>> GetAllCountries();
    Task<Country?> GetCountryById(Guid id);
}