using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sobczal1.KickBets.Application.DTOs.Identity;
using Sobczal1.KickBets.Application.Features.Identity.Requests.Commands;
using Sobczal1.KickBets.Application.Features.Identity.Requests.Queries;

namespace Sobczal1.KickBets.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
    {
        var user = await _mediator.Send(
            new LoginCommand {LoginDto = loginDto});
        return Ok(user);
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
    {
        var user = await _mediator.Send(
            new RegisterCommand {RegisterDto = registerDto});
        return Ok(user);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetCurrentUser([FromQuery] bool? refreshToken)
    {
        var user = await _mediator.Send(
            new GetCurrentUserQuery {RefreshToken = refreshToken});
        return Ok(user);
    }

    [HttpPost("addBalance")]
    [Authorize]
    public async Task<ActionResult<UserDto>> AddBalance()
    {
        var user = await _mediator.Send(
            new AddBalanceCommand());
        return Ok(user);
    }
}