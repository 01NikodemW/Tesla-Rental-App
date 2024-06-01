using Workshop.Domain.Entities;

namespace Workshop.Domain.Repositories;

public interface ICarsRepository
{
    Task<IEnumerable<Car>> GetAllAsync();
    Task<Car?> GetCarById(Guid id);
    Task<IEnumerable<Car>> GetAvailableCars(DateOnly rentalDate, DateOnly returnDate);
    Task<Guid> CreateAsync(Car car);
}