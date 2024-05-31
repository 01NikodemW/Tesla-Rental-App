using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workshop.Application.Countries.Dtos;
using Workshop.Application.Countries.Queries.GetAllCountries;

namespace Workshop.API.Controllers;

[ApiController]
[Route("api/countries")]
public class CountryController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CountryDto>>> GetAll([FromQuery] GetAllCountriesQuery query)
    {
        var countries = await mediator.Send(query);
        return Ok(countries);
    }
}