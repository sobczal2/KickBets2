using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sobczal1.KickBets.Application.DTOs.Football.Statistics;
using Sobczal1.KickBets.Application.Features.Statistics.Requests.Queries;

namespace Sobczal1.KickBets.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatisticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatisticsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StatisticDto>> GetById([FromRoute] int id)
    {
        var statistics =
            await _mediator.Send(new GetStatisticsByIdQuery
                {Id = id});
        return Ok(statistics);
    }
}