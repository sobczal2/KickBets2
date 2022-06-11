using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sobczal1.KickBets.Application.DTOs.Bets;
using Sobczal1.KickBets.Application.Features.BetsData.Requests.Queries;

namespace Sobczal1.KickBets.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BetsDataController : ControllerBase
{
    private readonly IMediator _mediator;

    public BetsDataController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BetsDataDto>> GetById([FromRoute] int id)
    {
        var betsData =
            await _mediator.Send(
                new GetBetsDataByIdQuery {Id = id});
        return Ok(betsData);
    }
}