using MediatR;
using Workshop.Application.Countries.Dtos;

namespace Workshop.Application.Countries.Queries.GetAllCountries;

public class GetAllCountriesQuery : IRequest<IEnumerable<CountryDto>>
{
}