using AutoMapper;
using Workshop.Application.Cars.Commands.CreateCar;
using Workshop.Domain.Entities;

namespace Workshop.Application.Cars.Dtos;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<Car, CarDto>();
        CreateMap<CreateCarCommand, Car>();
    }
}