using Workshop.Domain.Constants;
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

            if (!dbContext.Countries.Any())
            {
                var countries = GetCountries();
                dbContext.Countries.AddRange(countries);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Locations.Any())
            {
                var locations = GetLocations();
                dbContext.Locations.AddRange(locations);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Country> GetCountries()
    {
        var countries = new List<Country>
        {
            new()
            {
                Id = Guid.Parse("f1b5b1b0-5c1f-4b1f-9c1f-08d8e1b5b1b0"),
                Name = "Austria"
            },
            new()
            {
                Id = Guid.Parse("1faa6b15-6f8d-44a2-97e2-09a8b7118e6b"),
                Name = "Belgium"
            },
            new()
            {
                Id = Guid.Parse("78f725c3-c46a-45c3-9203-32a91f85330d"),
                Name = "Bulgaria"
            },
            new()
            {
                Id = Guid.Parse("5b5ef594-ebc9-47e9-84c5-56ef6821d49e"),
                Name = "Croatia"
            },
            new()
            {
                Id = Guid.Parse("d2a91b91-5b5a-4c26-8705-5c5a19ca84a8"),
                Name = "Cyprus"
            },
            new()
            {
                Id = Guid.Parse("e3189371-5742-42f8-b0e5-7566b1183836"),
                Name = "Czech Republic"
            },
            new()
            {
                Id = Guid.Parse("f338c1f1-0e94-4163-9869-9cfb7452dbd7"),
                Name = "Denmark"
            },
            new()
            {
                Id = Guid.Parse("7a837a61-3c24-438e-9f3b-20732e0c9887"),
                Name = "Estonia"
            },
            new()
            {
                Id = Guid.Parse("8de0e6e5-0597-47d8-a8c7-84f873a77ab7"),
                Name = "Finland"
            },
            new()
            {
                Id = Guid.Parse("bc003c31-94a4-4336-9e02-4d322f7b22a8"),
                Name = "France"
            },
            new()
            {
                Id = Guid.Parse("c0e95c11-48e8-4c68-bc38-95644d79cf20"),
                Name = "Germany"
            },
            new()
            {
                Id = Guid.Parse("c7d743aa-d91e-4a21-9d08-6b2b4315c4a4"),
                Name = "Greece"
            },
            new()
            {
                Id = Guid.Parse("c7e2a9b4-6f96-4df3-8a2d-56957c8ed700"),
                Name = "Hungary"
            },
            new()
            {
                Id = Guid.Parse("a5ef1a70-5d6f-4967-bc27-2f7386adbb8a"),
                Name = "Ireland"
            },
            new()
            {
                Id = Guid.Parse("b0f0b0a0-0f0b-4b0f-8b0f-0b0f0b0f0b0f"),
                Name = "Italy"
            },
            new()
            {
                Id = Guid.Parse("764fa43d-9d70-4deb-9b16-70b4eb3d13c6"),
                Name = "Latvia"
            },
            new()
            {
                Id = Guid.Parse("72d61b31-73b3-4d1b-a72f-07620f2ca927"),
                Name = "Lithuania"
            },
            new()
            {
                Id = Guid.Parse("e6bce5e6-05db-4f44-a4a6-69a3731ee084"),
                Name = "Luxembourg"
            },
            new()
            {
                Id = Guid.Parse("2ab9aa72-7f07-43cc-88f3-e95d79afdc9c"),
                Name = "Malta"
            },
            new()
            {
                Id = Guid.Parse("1e310798-b8bf-48dd-b64a-7fdd6e27d6b3"),
                Name = "Netherlands"
            },
            new()
            {
                Id = Guid.Parse("0d2d2c68-6fb8-4ccf-981a-2e9ecb3c559f"),
                Name = "Poland"
            },
            new()
            {
                Id = Guid.Parse("c2616203-4c46-4a7c-8eb2-c5d7859edce9"),
                Name = "Portugal"
            },
            new()
            {
                Id = Guid.Parse("1b8c1b10-4b8f-46e7-a3de-8cf7b89d3b49"),
                Name = "Romania"
            },
            new()
            {
                Id = Guid.Parse("0f12b48e-39a3-4f17-8a70-36bba97ee5e1"),
                Name = "Slovakia"
            },
            new()
            {
                Id = Guid.Parse("6f2943a8-850c-48f8-8a9b-d7e8c7b14a10"),
                Name = "Slovenia"
            },
            new()
            {
                Id = Guid.Parse("c4bbd2a5-df57-48f5-b04e-9a8e5c7a8173"),
                Name = "Spain"
            },
            new()
            {
                Id = Guid.Parse("81d1c534-8c39-4c68-b1bb-5d035d66e80e"),
                Name = "Sweden"
            },
        };

        return countries;
    }

    private static IEnumerable<Location> GetLocations()
    {
        var locations = new List<Location>()
        {
            new()
            {
                Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                Name = "Palma Airport"
            },
            new()
            {
                Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                Name = "Palma City Center"
            },
            new()
            {
                Id = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                Name = "Alcudia"
            },
            new()
            {
                Id = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09"),
                Name = "Manacor"
            },
        };


        return locations;
    }

    private IEnumerable<Car> GetCars()
    {
        var cars = new List<Car>
        {
            new()
            {
                Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                Model = TeslaModel._3,
                Mileage = 12742,
                ImageUrl = "https://storage.carsmile.pl/uploads/2021/10/thumb_tesla_sedan_31.jpg",
                RentalPricePerDay = 100,
            },
            new()
            {
                Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                Model = TeslaModel.S,
                Mileage = 12742,
                ImageUrl = "https://imagazine.pl/wp-content/uploads/2018/04/tesla-p100d.gif-hero.jpg",
                RentalPricePerDay = 250,
            },
            new()
            {
                Id = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                Model = TeslaModel.X,
                Mileage = 12742,
                ImageUrl = "https://img.chceauto.pl/tesla/model-x/tesla-model-x-suv-4038-41865_head.jpg",
                RentalPricePerDay = 300,
            },
            new()
            {
                Id = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09"),
                Model = TeslaModel.Y,
                Mileage = 12742,
                ImageUrl = "https://storage.carsmile.pl/uploads/2021/12/thumb_tesla_model_y.jpg",
                RentalPricePerDay = 350,
            },
        };

        return cars;
    }
}