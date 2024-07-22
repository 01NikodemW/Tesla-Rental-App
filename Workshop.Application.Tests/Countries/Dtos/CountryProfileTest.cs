using System;
using AutoMapper;
using JetBrains.Annotations;
using Workshop.Application.Countries.Dtos;
using Workshop.Domain.Entities;
using Xunit;

namespace Workshop.Application.Tests.Countries.Dtos;

[TestSubject(typeof(CountryProfile))]
public class CountryProfileTest
{
    private readonly IMapper _mapper;

    public CountryProfileTest()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<CountryProfile>());
        _mapper = new Mapper(configuration);
    }

    [Fact]
    public void CreateMap_ForCountryToCountryDto_MapsCorrectly()
    {
        // arrange
        var id = Guid.NewGuid();
        var name = "Country";
        var country = new Country()
        {
            Id = id,
            Name = name,
        };

        // act
        var result = _mapper.Map<CountryDto>(country);

        // assert
        Assert.Equal(country.Id, result.Id);
        Assert.Equal(country.Name, result.Name);
    }
}