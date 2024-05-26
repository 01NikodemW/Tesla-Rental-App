namespace Workshop.Application.Cars.Dtos;

public class CarDto
{
    public int Id { get; set; }
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
}