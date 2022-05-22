using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sobczal1.KickBets.Application.DTOs.Football.Fixtures;
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
    public async Task<ActionResult<PaginatedResponse<FixtureDto>>> List([FromQuery] PaginatedRequestData paginatedRequestData, [FromQuery] FixtureListParams fixtureListParams)
    {
        var fixtures =
            await _mediator.Send(
                new Sobczal1.KickBets.Application.Features.Fixtures.Requests.Queries.GetFixtureListQuery
                    {PaginatedRequestData = paginatedRequestData, FixtureListParams = fixtureListParams});
        return Ok(fixtures);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<FixtureDto>> GetById([FromRoute] int id)
    {
        var fixture =
            await _mediator.Send(
                new Sobczal1.KickBets.Application.Features.Fixtures.Requests.Queries.GetFixtureByIdQuery {Id = id});
        return Ok(fixture);
    }
}