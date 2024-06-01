using AutoMapper;
using Workshop.Domain.Entities;

namespace Workshop.Application.Countries.Dtos;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<Country, CountryDto>();
    }
}