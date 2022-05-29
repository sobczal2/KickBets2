using MediatR;
using Sobczal1.KickBets.Application.DTOs.Bets;

namespace Sobczal1.KickBets.Application.Features.BetsData.Requests.Queries;

public class GetBetsDataByIdQuery : IRequest<BetsDataDto>
{
    public int Id { get; set; }
}