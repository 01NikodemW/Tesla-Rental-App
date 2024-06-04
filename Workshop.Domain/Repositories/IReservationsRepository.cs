using Workshop.Domain.Constants;
using Workshop.Domain.Entities;

namespace Workshop.Domain.Repositories;

public interface IReservationsRepository
{
    Task<Guid> CreateReservation(Reservation reservation, CancellationToken cancellationToken);

    Task<(IEnumerable<Reservation>, int)> GetReservationsByUserId(Guid userId, int pageSize,
        int pageNumber, string? sortBy,
        SortDirection sortDirection, CancellationToken cancellationToken);
}