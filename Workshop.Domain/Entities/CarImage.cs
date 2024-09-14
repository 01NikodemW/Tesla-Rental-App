namespace Workshop.Domain.Entities;

public class CarImage
{
    public Guid Id { get; set; }
    public string Path { get; set; }
    public Guid CarId { get; set; }
    public virtual Car Car { get; set; }
    public Guid CreatedBy { get; set; }
    public virtual User CreatedByUser { get; set; }
    public DateTime CreatedAt { get; set; }
}