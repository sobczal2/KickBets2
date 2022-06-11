using AutoMapper;
using MediatR;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.DTOs.Bets;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.BetsData.Requests.Queries;

namespace Sobczal1.KickBets.Application.Features.BetsData.Handlers.Queries;

public class GetBetsDataByIdQueryHandler : IRequestHandler<GetBetsDataByIdQuery, BetsDataDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetBetsDataByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BetsDataDto> Handle(GetBetsDataByIdQuery query, CancellationToken cancellationToken)
    {
        var betsData = await _unitOfWork.BetsDataRepository.Get(query.Id);
        if (betsData is null)
            throw new NotFoundException("id", $"BetsData with id: {query.Id} not found.");
        return _mapper.Map<BetsDataDto>(betsData);
    }
}