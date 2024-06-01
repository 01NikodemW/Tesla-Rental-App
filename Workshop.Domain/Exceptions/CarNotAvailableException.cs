namespace Workshop.Domain.Exceptions;

public class CarNotAvailableException(string carId, DateOnly rentalDate, DateOnly returnDate)
    : Exception($"Car with id: {carId} is not available for rental from {rentalDate} to {returnDate}")
{
}