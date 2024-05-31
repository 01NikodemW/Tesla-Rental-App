using Workshop.Domain.Constants;

namespace Workshop.Application.Cars.Dtos;

public class CarDto
{
    public Guid Id { get; set; }
    public TeslaModel Model { get; set; }
    public float Mileage { get; set; }
    public string ImageUrl { get; set; }
    public int RentalPricePerDay { get; set; }
}