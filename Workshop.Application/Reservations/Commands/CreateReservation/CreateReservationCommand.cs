using MediatR;

namespace Workshop.Application.Reservations.Commands.CreateReservation;

public class CreateReservationCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public Guid CarId { get; set; }
    public Guid RentalLocationId { get; set; }
    public Guid ReturnLocationId { get; set; }
    public DateOnly RentalDate { get; set; }
    public DateOnly ReturnDate { get; set; }
}