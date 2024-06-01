using Workshop.Domain.Constants;

namespace Workshop.Domain.Entities;

public class Car
{
    public Guid Id { get; set; }
    public TeslaModel Model { get; set; }
    public float Mileage { get; set; }
    public string ImageUrl { get; set; }
    public int RentalPricePerDay { get; set; }


    public int CalculatePrice(DateOnly rentalDate, DateOnly returnDate)
    {
        var days = (returnDate.DayNumber - rentalDate.DayNumber) + 1;
        return days * RentalPricePerDay;
    }
}