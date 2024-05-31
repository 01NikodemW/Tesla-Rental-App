using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Workshop.Application.Countries.Dtos;
using Workshop.Domain.Repositories;

namespace Workshop.Application.Countries.Queries.GetAllCountries;

public class GetAllCountriesQueryHandler(
    ILogger<GetAllCountriesQuery> logger,
    ICountriesRepository countriesRepository,
    IMapper mapper) : IRequestHandler<GetAllCountriesQuery, IEnumerable<CountryDto>>
{
    public async Task<IEnumerable<CountryDto>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all countries");
        var countries = await countriesRepository.GetAllCountries();
        var result = mapper.Map<IEnumerable<CountryDto>>(countries);
        return result;
    }
}