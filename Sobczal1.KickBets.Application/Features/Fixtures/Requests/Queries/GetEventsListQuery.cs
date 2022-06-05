using MediatR;
using Sobczal1.KickBets.Application.DTOs.Football.Events;

namespace Sobczal1.KickBets.Application.Features.Fixtures.Requests.Queries;

public class GetEventsListQuery : IRequest<List<EventDto>>
{
    public int? FixtureId { get; set; }
}