using Workshop.Domain.Entities;
using Workshop.Infrastructure.Persistence;

namespace Workshop.Infrastructure.Seeders;

internal class WorkshopSeeder(WorkshopDbContext dbContext) : IWorkshopSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Cars.Any())
            {
                var cars = GetCars();
                dbContext.Cars.AddRange(cars);
                await dbContext.SaveChangesAsync();
            }
        }
    }


    private IEnumerable<Car> GetCars()
    {
        var cars = new List<Car>
        {
            new()
            {
                Brand = "Audo",
                Model = "A7"
            },
        };

        return cars;
    }
}