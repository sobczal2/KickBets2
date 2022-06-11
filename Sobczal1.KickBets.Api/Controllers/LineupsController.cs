using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sobczal1.KickBets.Application.DTOs.Football.Lineups;
using Sobczal1.KickBets.Application.DTOs.Football.Players;
using Sobczal1.KickBets.Application.Features.Lineups.Requests.Queries;
using Sobczal1.KickBets.Application.Features.Players.Requests.Queries;

namespace Sobczal1.KickBets.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LineupsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LineupsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LineupDto>> GetById([FromRoute] int id)
    {
        var lineup =
            await _mediator.Send(new GetLineupByIdQuery
                {Id = id});
        return Ok(lineup);
    }

    [HttpGet("{id:int}/players")]
    public async Task<ActionResult<List<PlayerDto>>> ListPlayersByLineupId([FromRoute] int id)
    {
        var players =
            await _mediator.Send(
                new GetPlayersByLineupIdQuery
                    {LineupId = id});
        return Ok(players);
    }
}