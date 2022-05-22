using MediatR;
using Sobczal1.KickBets.Application.DTOs.Football.Players;

namespace Sobczal1.KickBets.Application.Features.Players.Requests.Queries;

public class GetPlayersByLineupIdQuery : IRequest<List<PlayerDto>>
{
    public int? LineupId { get; set; }
}