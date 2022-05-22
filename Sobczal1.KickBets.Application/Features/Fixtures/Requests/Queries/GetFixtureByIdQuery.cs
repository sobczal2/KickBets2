using MediatR;
using Sobczal1.KickBets.Application.DTOs.Football.Fixtures;

namespace Sobczal1.KickBets.Application.Features.Fixtures.Requests.Queries;

public class GetFixtureByIdQuery : IRequest<FixtureDto>
{
    public int Id { get; set; }
}