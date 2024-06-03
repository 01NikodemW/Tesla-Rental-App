using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Workshop.Application.Reservations.Dtos;
using Workshop.Domain.Repositories;

namespace Workshop.Application.Users.Queries.GetUserReservations;

public class GetUserReservationsQueryHandler(
    ILogger<GetUserReservationsQueryHandler> logger,
    IReservationsRepository reservationsRepository,
    IMapper mapper
) : IRequestHandler<GetUserReservationsQuery, IEnumerable<ReservationDto>>
{
    public async Task<IEnumerable<ReservationDto>> Handle(GetUserReservationsQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting reservations for user with id: {UserId}", request.UserId);
        var reservations = await reservationsRepository.GetReservationsByUserId(request.UserId, cancellationToken);
        var result = mapper.Map<IEnumerable<ReservationDto>>(reservations);
        return result;
    }
}