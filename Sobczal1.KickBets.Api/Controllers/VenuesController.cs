using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sobczal1.KickBets.Application.DTOs.Football.Venues;

namespace Sobczal1.KickBets.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VenuesController : ControllerBase
{
    private readonly IMediator _mediator;

    public VenuesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<VenueDto>> GetTeamByFixtureId([FromRoute] int id)
    {
        var venue = await _mediator.Send(
            new Sobczal1.KickBets.Application.Features.Venues.Requests.Queries.GetVenueByIdQuery {Id = id});
        return Ok(venue);
    }
}