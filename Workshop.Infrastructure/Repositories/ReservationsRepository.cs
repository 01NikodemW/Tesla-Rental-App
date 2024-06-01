using Workshop.Domain.Entities;
using Workshop.Domain.Repositories;
using Workshop.Infrastructure.Persistence;

namespace Workshop.Infrastructure.Repositories;

public class ReservationsRepository(WorkshopDbContext dbContext) : IReservationsRepository
{
    public async Task<Guid> CreateReservation(Reservation reservation)
    {
        dbContext.Reservations.Add(reservation);
        await dbContext.SaveChangesAsync();
        return reservation.Id;
    }
}