using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sobczal1.KickBets.Application.DTOs.Bets;
using Sobczal1.KickBets.Application.Features.Bets.Requests.Commands;
using Sobczal1.KickBets.Application.Features.Bets.Requests.Queries;
using Sobczal1.KickBets.Application.Models.Pagination;

namespace Sobczal1.KickBets.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BetsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BetsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("wdlft")]
    [Authorize]
    public async Task<ActionResult> CreateWdlftBet([FromBody] CreateWdlftBetDto createWdlftBetDto)
    {
        await _mediator.Send(
            new CreateWdlftBetCommand
                {CreateWdlftBetDto = createWdlftBetDto});
        return Ok();
    }

    [HttpPost("wdlht")]
    [Authorize]
    public async Task<ActionResult> CreateWdlhtBet([FromBody] CreateWdlhtBetDto createWdlhtBetDto)
    {
        await _mediator.Send(
            new CreateWdlhtBetCommand
                {CreateWdlhtBetDto = createWdlhtBetDto});
        return Ok();
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<PaginatedResponse<BaseBetDto>>> GetMyBets(
        [FromQuery] PaginatedRequestData paginatedRequestData)
    {
        var bets = await _mediator.Send(
            new GetMyBetListQuery
                {PaginatedRequestData = paginatedRequestData});
        return Ok(bets);
    }
}