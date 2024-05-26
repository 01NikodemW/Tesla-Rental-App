using Microsoft.EntityFrameworkCore;
using Workshop.Domain.Entities;
using Workshop.Domain.Repositories;
using Workshop.Infrastructure.Persistence;

namespace Workshop.Infrastructure.Repositories;

internal class CarsRepository(WorkshopDbContext dbContext) : ICarsRepository
{
    public async Task<IEnumerable<Car>> GetAllAsync()
    {
        var cars = await dbContext.Cars.ToListAsync();
        return cars;
    }

    public async Task<int> CreateAsync(Car car)
    {
        dbContext.Cars.Add(car);
        await dbContext.SaveChangesAsync();
        return car.Id;
    }
}