namespace Workshop.Domain.Entities;

public class Reservation
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public Guid CarId { get; set; }
    public virtual Car Car { get; set; }
    public Guid RentalLocationId { get; set; }
    public virtual Location RentalLocation { get; set; }
    public Guid ReturnLocationId { get; set; }
    public virtual Location ReturnLocation { get; set; }
    public DateOnly RentalDate { get; set; }
    public DateOnly ReturnDate { get; set; }
    public int TotalPrice { get; set; }
}