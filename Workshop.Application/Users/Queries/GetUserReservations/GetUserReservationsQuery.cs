using MediatR;
using Workshop.Application.Reservations.Dtos;

namespace Workshop.Application.Users.Queries.GetUserReservations;

public class GetUserReservationsQuery : IRequest<IEnumerable<ReservationDto>>
{
    public Guid UserId { get; set; }
}