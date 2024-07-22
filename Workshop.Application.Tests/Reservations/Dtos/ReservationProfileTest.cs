using System;
using AutoMapper;
using JetBrains.Annotations;
using Workshop.Application.Cars.Dtos;
using Workshop.Application.Locations.Dtos;
using Workshop.Application.Reservations.Dtos;
using Workshop.Domain.Entities;
using Xunit;

namespace Workshop.Application.Tests.Reservations.Dtos;

[TestSubject(typeof(ReservationProfile))]
public class ReservationProfileTest
{
    private readonly IMapper _mapper;

    public ReservationProfileTest()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ReservationProfile>());
        _mapper = new Mapper(configuration);
    }

    [Fact]
    public void CreateMap_ForReservationToReservationDto_MapsCorrectly()
    {
        
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var carId = Guid.NewGuid();
        var rentalLocationId = Guid.NewGuid();
        var returnLocationId = Guid.NewGuid();

        // arrange
        var reservation = new Reservation()
        {
            Id = id,
            UserId = userId,
            CarId = carId,
            RentalLocationId = rentalLocationId,
            ReturnLocationId = returnLocationId,
            RentalDate = new DateOnly(2022, 1, 1),
            ReturnDate = new DateOnly(2022, 1, 2),
            TotalPrice = 100
        };
        
        var expectedCarDto = new CarDto { Id = carId };
        var expectedRentalLocationDto = new LocationDto { Id = rentalLocationId };
        var expectedReturnLocationDto = new LocationDto { Id = returnLocationId };
        
        // act
        var result = _mapper.Map<ReservationDto>(reservation);
 

        result.Car = expectedCarDto;
        result.RentalLocation = expectedRentalLocationDto;
        result.ReturnLocation = expectedReturnLocationDto;
        
        
        // assert
        Assert.Equal(reservation.Id, result.Id);
        Assert.Equal(reservation.UserId, result.UserId);
        Assert.Equal(reservation.CarId, result.Car.Id);
        Assert.Equal(reservation.RentalLocationId, result.RentalLocation.Id);
        Assert.Equal(reservation.ReturnLocationId, result.ReturnLocation.Id);
        Assert.Equal(reservation.RentalDate, result.RentalDate);
        Assert.Equal(reservation.ReturnDate, result.ReturnDate);
        Assert.Equal(reservation.TotalPrice, result.TotalPrice);
        
        
    }
}