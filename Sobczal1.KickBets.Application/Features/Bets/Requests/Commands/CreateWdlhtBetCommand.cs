using MediatR;
using Sobczal1.KickBets.Application.DTOs.Bets;

namespace Sobczal1.KickBets.Application.Features.Bets.Requests.Commands;

public class CreateWdlhtBetCommand : IRequest<Unit>
{
    public CreateWdlhtBetDto CreateWdlhtBetDto { get; set; } = null!;
}