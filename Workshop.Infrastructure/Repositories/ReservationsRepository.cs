using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Workshop.Domain.Constants;
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

    public async Task<(IEnumerable<Reservation>, int)> GetReservationsByUserId(Guid userId,
        int pageSize, int pageNumber, string? sortBy,
        SortDirection sortDirection, CancellationToken cancellationToken)
    {
        var baseQuery = dbContext
            .Reservations
            .Include(x => x.Car)
            .Include(x => x.RentalLocation)
            .Include(x => x.ReturnLocation)
            .Where(x => x.UserId == userId);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Reservation, object>>>
            {
                { nameof(Reservation.Car), r => r.Car.Model },
                { nameof(Reservation.RentalLocation), r => r.RentalLocation.Name },
                { nameof(Reservation.ReturnLocation), r => r.ReturnLocation.Name },
                { nameof(Reservation.RentalDate), r => r.RentalDate },
                { nameof(Reservation.ReturnDate), r => r.ReturnDate },
                { nameof(Reservation.TotalPrice), r => r.TotalPrice }
            };

            if (columnsSelector.ContainsKey(sortBy))
            {
                var selectedColumn = columnsSelector[sortBy];
                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }
        }

        var reservations = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (reservations, totalCount);
    }
}