using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sobczal1.KickBets.Application.DTOs.Football.Status;

namespace Sobczal1.KickBets.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatusesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StatusDto>> GetById([FromRoute] int id)
    {
        var status =
            await _mediator.Send(new Sobczal1.KickBets.Application.Features.Statuses.Requests.Queries.GetStatusByIdQuery
                {Id = id});
        return Ok(status);
    }
}