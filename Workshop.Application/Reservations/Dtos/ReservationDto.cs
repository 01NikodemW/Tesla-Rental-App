using Workshop.Application.Cars.Dtos;
using Workshop.Application.Locations.Dtos;

namespace Workshop.Application.Reservations.Dtos;

public class ReservationDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public CarDto Car { get; set; }
    public LocationDto RentalLocation { get; set; }
    public LocationDto ReturnLocation { get; set; }
    public DateOnly RentalDate { get; set; }
    public DateOnly ReturnDate { get; set; }
    public int TotalPrice { get; set; }
}