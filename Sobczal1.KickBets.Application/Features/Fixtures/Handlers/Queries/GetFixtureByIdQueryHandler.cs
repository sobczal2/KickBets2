using AutoMapper;
using MediatR;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.DTOs.Football.Fixtures;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Fixtures.Requests.Queries;

namespace Sobczal1.KickBets.Application.Features.Fixtures.Handlers.Queries;

public class GetFixtureByIdQueryHandler : IRequestHandler<GetFixtureByIdQuery, FixtureDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetFixtureByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<FixtureDto> Handle(GetFixtureByIdQuery query, CancellationToken cancellationToken)
    {
        var fixture = await _unitOfWork.FixtureRepository.Get(query.Id);
        if (fixture is null)
            throw new NotFoundException("id", $"Fixture with id: {query.Id} not found.");
        return _mapper.Map<FixtureDto>(fixture);
    }
}