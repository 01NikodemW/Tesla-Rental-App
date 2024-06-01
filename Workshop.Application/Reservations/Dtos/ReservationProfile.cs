using AutoMapper;
using Workshop.Application.Reservations.Commands.CreateReservation;
using Workshop.Domain.Entities;

namespace Workshop.Application.Reservations.Dtos;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<CreateReservationCommand, Reservation>();
        CreateMap<Reservation, ReservationDto>();
    }
}