using Workshop.Domain.Entities;

namespace Workshop.Domain.Repositories;

public interface ICountriesRepository
{
    Task<IEnumerable<Country>> GetAllCountries(CancellationToken cancellationToken);
    Task<Country?> GetCountryById(Guid id, CancellationToken cancellationToken);

    bool CheckIfCountryWithProvidedIdInDb(Guid id);
}