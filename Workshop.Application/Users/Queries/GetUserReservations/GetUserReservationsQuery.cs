using MediatR;
using Workshop.Application.Common;
using Workshop.Application.Reservations.Dtos;
using Workshop.Domain.Constants;

namespace Workshop.Application.Users.Queries.GetUserReservations;

public class GetUserReservationsQuery : IRequest<PagedResult<ReservationDto>>
{
    public Guid UserId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}