using MediatR;
using Sobczal1.KickBets.Application.DTOs.Football.Events;

namespace Sobczal1.KickBets.Application.Features.Events.Requests.Queries;

public class GetEventsByFixtureIdQuery : IRequest<EventDto>
{
    public int FixtureId { get; set; }
}