using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sobczal1.KickBets.Application.DTOs.Football.Team;
using Sobczal1.KickBets.Application.Features.Teams.Requests.Queries;

namespace Sobczal1.KickBets.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeamsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TeamDto>> GetTeamByFixtureId([FromRoute] int id)
    {
        var team = await _mediator.Send(
            new GetTeamByIdQuery {Id = id});
        return Ok(team);
    }
}