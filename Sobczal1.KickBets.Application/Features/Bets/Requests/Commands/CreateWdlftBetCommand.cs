using MediatR;
using Sobczal1.KickBets.Application.DTOs.Bets;

namespace Sobczal1.KickBets.Application.Features.Bets.Requests.Commands;

public class CreateWdlftBetCommand : IRequest<Unit>
{
    public CreateWdlftBetDto CreateWdlftBetDto { get; set; } = null!;
}