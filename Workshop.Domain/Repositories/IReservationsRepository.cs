using Workshop.Domain.Entities;

namespace Workshop.Domain.Repositories;

public interface IReservationsRepository
{
    Task<Guid> CreateReservation(Reservation reservation, CancellationToken cancellationToken);
    Task<IEnumerable<Reservation>> GetReservationsByUserId(Guid userId, CancellationToken cancellationToken);
}