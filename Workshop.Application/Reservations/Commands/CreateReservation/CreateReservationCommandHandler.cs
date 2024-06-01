using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Workshop.Domain.Entities;
using Workshop.Domain.Exceptions;
using Workshop.Domain.Repositories;

namespace Workshop.Application.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler(
    ILogger<CreateReservationCommandHandler> logger,
    ICarsRepository carsRepository,
    ILocationsRepository locationsRepository,
    IReservationsRepository reservationsRepository,
    IMapper mapper
) : IRequestHandler<CreateReservationCommand, Guid>
{
    public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Creating a reservation for car with id {CarId}, rental date: {RentalDate}, " +
            "rental location: {RentalLocation}, return date: {ReturnDate}, return location: {ReturnLocation}",
            request.CarId,
            request.RentalDate,
            request.RentalLocationId,
            request.ReturnDate,
            request.ReturnLocationId
        );

        var car = await carsRepository.GetCarById(request.CarId);
        if (car is null) throw new NotFoundException(nameof(Car), request.CarId.ToString());

        var rentalLocation = await locationsRepository.GetLocationById(request.RentalLocationId);
        if (rentalLocation is null) throw new NotFoundException(nameof(Location), request.RentalLocationId.ToString());

        var returnLocation = await locationsRepository.GetLocationById(request.ReturnLocationId);
        if (returnLocation is null) throw new NotFoundException(nameof(Location), request.ReturnLocationId.ToString());


        var cars = await carsRepository.GetAvailableCars(request.RentalDate, request.ReturnDate);

        if (!cars.Any(c => c.Id == request.CarId))
        {
            throw new CarNotAvailableException(request.CarId.ToString(), request.RentalDate, request.ReturnDate);
        }

        var totalPrice = car.CalculatePrice(request.RentalDate, request.ReturnDate);

        var reservation = mapper.Map<Reservation>(request);
        reservation.TotalPrice = totalPrice;
        var reservationId = await reservationsRepository.CreateReservation(reservation);
        return reservationId;
    }
}