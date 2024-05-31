using Workshop.Domain.Entities;

namespace Workshop.Domain.Repositories;

public interface ICarsRepository
{
    Task<IEnumerable<Car>> GetAllAsync();
    Task<Guid> CreateAsync(Car car);
}