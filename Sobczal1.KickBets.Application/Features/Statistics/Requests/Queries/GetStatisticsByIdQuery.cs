using MediatR;
using Sobczal1.KickBets.Application.DTOs.Football.Statistics;

namespace Sobczal1.KickBets.Application.Features.Statistics.Requests.Queries;

public class GetStatisticsByIdQuery : IRequest<StatisticDto>
{
    public int Id { get; set; }
}