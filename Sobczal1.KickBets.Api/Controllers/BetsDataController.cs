using MediatR;
using Microsoft.AspNetCore.Mvc;

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
}