using MediatR;
using Sobczal1.KickBets.Application.DTOs.Football.Venues;

namespace Sobczal1.KickBets.Application.Features.Venues.Requests.Queries;

public class GetVenueByIdQuery : IRequest<VenueDto>
{
    public int Id { get; set; }
}