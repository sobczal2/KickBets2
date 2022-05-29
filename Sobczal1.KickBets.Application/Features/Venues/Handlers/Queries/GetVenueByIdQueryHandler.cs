using AutoMapper;
using MediatR;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.DTOs.Football.Team;
using Sobczal1.KickBets.Application.DTOs.Football.Venues;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Venues.Requests.Queries;

namespace Sobczal1.KickBets.Application.Features.Venues.Handlers.Queries;

public class GetVenueByIdQueryHandler : IRequestHandler<GetVenueByIdQuery, VenueDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetVenueByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<VenueDto> Handle(GetVenueByIdQuery query, CancellationToken cancellationToken)
    {
        var venue = await _unitOfWork.VenueRepository.Get(query.Id);
        if(venue is null)
            throw new NotFoundException("id", $"Venue with id: {query.Id} not found.");
        return _mapper.Map<VenueDto>(venue);
    }
}