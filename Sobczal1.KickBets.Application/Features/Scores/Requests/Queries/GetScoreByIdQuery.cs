using MediatR;
using Sobczal1.KickBets.Application.DTOs.Football.Scores;

namespace Sobczal1.KickBets.Application.Features.Scores.Requests.Queries;

public class GetScoreByIdQuery : IRequest<ScoreDto>
{
    public int Id { get; set; }
}