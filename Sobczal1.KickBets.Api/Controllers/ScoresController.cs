using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sobczal1.KickBets.Application.DTOs.Football.Scores;
using Sobczal1.KickBets.Application.Features.Scores.Requests.Queries;

namespace Sobczal1.KickBets.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScoresController : ControllerBase
{
    private readonly IMediator _mediator;

    public ScoresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ScoreDto>> GetById([FromRoute] int id)
    {
        var score =
            await _mediator.Send(new GetScoreByIdQuery
                {Id = id});
        return Ok(score);
    }
}