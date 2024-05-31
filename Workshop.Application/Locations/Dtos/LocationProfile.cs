using AutoMapper;
using Workshop.Domain.Entities;

namespace Workshop.Application.Locations.Dtos;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, LocationDto>();
    }
}