using Microsoft.EntityFrameworkCore;
using Workshop.Application.Reservations.Dtos;
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

    public async Task<Car?> GetCarById(Guid id)
    {
        var car = await dbContext.Cars.FindAsync(id);
        return car;
    }

    public async Task<Guid> CreateAsync(Car car)
    {
        dbContext.Cars.Add(car);
        await dbContext.SaveChangesAsync();
        return car.Id;
    }

    public async Task<IEnumerable<Car>> GetAvailableCars(DateOnly rentalDate, DateOnly returnDate)
    {
        var availableCars = new List<Car>();
        var cars = await dbContext.Cars.ToListAsync();
        foreach (var car in cars)
        {
            var reservationDates = await GetReservationDatesByCar(car.Id);
            var isAvailable = true;

            foreach (var reservationDate in reservationDates)
            {
                if (rentalDate >= reservationDate.RentalDate &&
                    rentalDate <= reservationDate.ReturnDate)
                {
                    isAvailable = false;
                    break;
                }

                if (returnDate >= reservationDate.RentalDate &&
                    returnDate <= reservationDate.ReturnDate)
                {
                    isAvailable = false;
                    break;
                }

                if (rentalDate <= reservationDate.RentalDate &&
                    returnDate >= reservationDate.ReturnDate)
                {
                    isAvailable = false;
                    break;
                }
            }

            if (isAvailable)
            {
                availableCars.Add(car);
            }
        }


        return availableCars;
    }

    private async Task<IEnumerable<ReservationDatesDto>> GetReservationDatesByCar(Guid carId)
    {
        var reservations = await dbContext.Reservations
            .Where(x => x.CarId == carId)
            .ToListAsync();

        var reservationDates = new List<ReservationDatesDto>();

        foreach (var reservation in reservations)
        {
            var reservationDate = new ReservationDatesDto()
            {
                RentalDate = reservation.RentalDate,
                ReturnDate = reservation.ReturnDate
            };

            reservationDates.Add(reservationDate);
        }

        return reservationDates;
    }
}