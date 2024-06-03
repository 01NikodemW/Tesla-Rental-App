using Microsoft.EntityFrameworkCore;
using Workshop.Domain.Entities;
using Workshop.Domain.Repositories;
using Workshop.Infrastructure.Persistence;

namespace Workshop.Infrastructure.Repositories;

public class ReservationsRepository(WorkshopDbContext dbContext) : IReservationsRepository
{
    public async Task<Guid> CreateReservation(Reservation reservation, CancellationToken cancellationToken)
    {
        dbContext.Reservations.Add(reservation);
        await dbContext.SaveChangesAsync(cancellationToken);
        return reservation.Id;
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByUserId(Guid userId,
        CancellationToken cancellationToken)
    {
        var reservations = await dbContext.Reservations
            .Include(x => x.Car)
            .Include(x => x.RentalLocation)
            .Include(x => x.ReturnLocation)
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);

        return reservations;
    }
}