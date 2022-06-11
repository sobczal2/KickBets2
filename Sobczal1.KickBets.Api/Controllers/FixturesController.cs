using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sobczal1.KickBets.Application.DTOs.Football.Events;
using Sobczal1.KickBets.Application.DTOs.Football.Fixtures;
using Sobczal1.KickBets.Application.Features.Fixtures.Requests.Queries;
using Sobczal1.KickBets.Application.Models.Pagination;

namespace Sobczal1.KickBets.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FixturesController : ControllerBase
{
    private readonly IMediator _mediator;

    public FixturesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<FixtureDto>>> List(
        [FromQuery] PaginatedRequestData paginatedRequestData, [FromQuery] FixtureListParams fixtureListParams)
    {
        var fixtures =
            await _mediator.Send(
                new GetFixtureListQuery
                    {PaginatedRequestData = paginatedRequestData, FixtureListParams = fixtureListParams});
        return Ok(fixtures);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<FixtureDto>> GetById([FromRoute] int id)
    {
        var fixture =
            await _mediator.Send(
                new GetFixtureByIdQuery {Id = id});
        return Ok(fixture);
    }

    [HttpGet("{id:int}/events")]
    public async Task<ActionResult<List<EventDto>>> ListEventsByFixtureId([FromRoute] int id)
    {
        var events =
            await _mediator.Send(
                new GetEventsListQuery {FixtureId = id});
        return Ok(events);
    }
}