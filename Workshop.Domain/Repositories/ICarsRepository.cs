using Workshop.Domain.Entities;

namespace Workshop.Domain.Repositories;

public interface ICarsRepository
{
    Task<IEnumerable<Car>> GetAllAsync(CancellationToken cancellationToken);
    Task<Car?> GetCarById(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<Car>> GetAvailableCars(DateOnly rentalDate, DateOnly returnDate,
        CancellationToken cancellationToken);

    Task<Guid> CreateAsync(Car car, CancellationToken cancellationToken);
}