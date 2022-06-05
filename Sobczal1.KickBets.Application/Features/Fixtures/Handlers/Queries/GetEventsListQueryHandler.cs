using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.DTOs.Football.Events;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Fixtures.Requests.Queries;

namespace Sobczal1.KickBets.Application.Features.Fixtures.Handlers.Queries;

public class GetEventsListQueryHandler : IRequestHandler<GetEventsListQuery, List<EventDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEventsListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<EventDto>> Handle(GetEventsListQuery query, CancellationToken cancellationToken)
    {
        if (!query.FixtureId.HasValue)
            throw new BadRequestException(new Dictionary<string, string>{{"FixtureId", "FixtureId must not be null."}});
        if (!await _unitOfWork.FixtureRepository.Exists(query.FixtureId.Value))
            throw new NotFoundException("FixtureId", $"Fixture with id: {query.FixtureId} not found.");
        
        var events = await (await _unitOfWork.EventRepository.GetAll()).Where(p => p.FixtureId == query.FixtureId)
            .ToListAsync();

        return _mapper.Map<List<EventDto>>(events);
    }
}