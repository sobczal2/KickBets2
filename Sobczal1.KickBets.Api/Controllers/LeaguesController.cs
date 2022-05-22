
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sobczal1.KickBets.Application.DTOs.Football.Leagues;
using Sobczal1.KickBets.Application.Models.Pagination;

namespace Sobczal1.KickBets.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaguesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaguesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<LeagueDto>>> List([FromQuery] PaginatedRequestData paginatedRequestData)
    {
        var leagues =
            await _mediator.Send(new Sobczal1.KickBets.Application.Features.Leagues.Requests.Queries.GetLeagueListQuery
                {PaginatedRequestData = paginatedRequestData});
        return Ok(leagues);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LeagueDto>> GetById([FromRoute] int id)
    {
        var fixture =
            await _mediator.Send(
                new Sobczal1.KickBets.Application.Features.Leagues.Requests.Queries.GetLeagueByIdQuery{Id = id});
        return Ok(fixture);
    }
}