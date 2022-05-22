using MediatR;
using Sobczal1.KickBets.Application.DTOs.Football.Status;

namespace Sobczal1.KickBets.Application.Features.Statuses.Requests.Queries;

public class GetStatusByIdQuery : IRequest<StatusDto>
{
    public int Id { get; set; }
}