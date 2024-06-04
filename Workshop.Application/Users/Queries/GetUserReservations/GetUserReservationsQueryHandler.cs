using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Workshop.Application.Common;
using Workshop.Application.Reservations.Dtos;
using Workshop.Domain.Repositories;

namespace Workshop.Application.Users.Queries.GetUserReservations;

public class GetUserReservationsQueryHandler(
    ILogger<GetUserReservationsQueryHandler> logger,
    IReservationsRepository reservationsRepository,
    IMapper mapper
) :
    IRequestHandler<GetUserReservationsQuery, PagedResult<ReservationDto>>
{
    public async Task<PagedResult<ReservationDto>> Handle(GetUserReservationsQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting reservations for user with id: {UserId}", request.UserId);

        var (reservations, totalCount) = await reservationsRepository.GetReservationsByUserId(request.UserId,
            request.PageSize,
            request.PageNumber,
            request.SortBy,
            request.SortDirection, cancellationToken);

        var mappedReservationDtos = mapper.Map<IEnumerable<ReservationDto>>(reservations);

        var result =
            new PagedResult<ReservationDto>(mappedReservationDtos, totalCount, request.PageSize, request.PageNumber);
        return result;
    }
}