using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.DTOs.Football.Fixtures;
using Sobczal1.KickBets.Application.DTOs.Football.Fixtures.Validators;
using Sobczal1.KickBets.Application.Exceptions;
using Sobczal1.KickBets.Application.Features.Fixtures.Requests.Queries;
using Sobczal1.KickBets.Application.Models.Pagination;

namespace Sobczal1.KickBets.Application.Features.Fixtures.Handlers.Queries;

public class GetFixtureListQueryHandler : IRequestHandler<GetFixtureListQuery, PaginatedResponse<FixtureDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetFixtureListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<PaginatedResponse<FixtureDto>> Handle(GetFixtureListQuery query, CancellationToken cancellationToken)
    {
        var validator = new FixtureListParamsDtoValidator(_unitOfWork.LeagueRepository);
        var validationResult = await validator.ValidateAsync(query.FixtureListParams, cancellationToken);
        if (validationResult.IsValid == false)
            throw new ValidationErrorsException(validationResult);
        
        var baseQuery = await _unitOfWork.FixtureRepository.GetAllWithStatus();

        Expression<Func<FixtureDto, object>> order = q => q.Date;
        var reverseOrder = false;

        var upcoming = new string[] {"TBD", "NS", "1H", "HT", "2H", "ET", "P", "BT", "LIVE"};
        var cancelled = new string[] {"SUSP", "INT", "PST", "CANC", "ABD", "AWD", "WO"};
        var ended = new string[] {"FT", "AET", "PEN"};

        switch (query.FixtureListParams.Type)
        {
            case "upcoming":
                baseQuery = baseQuery.Where(f => upcoming.Contains(f.Status.Short.ToUpper()));
                break;
            case "cancelled":
                baseQuery = baseQuery.Where(f => cancelled.Contains(f.Status.Short.ToUpper()));
                reverseOrder = true;
                break;
            case "ended":
                baseQuery = baseQuery.Where(f => ended.Contains(f.Status.Short.ToUpper()));
                reverseOrder = true;
                break;
        }

        if (query.FixtureListParams.LeagueId.HasValue)
        {
            baseQuery = baseQuery.Where(f => f.LeagueId == query.FixtureListParams.LeagueId.Value);
        }
        
        return await PaginatedResponse<FixtureDto>.CreateAsync(
            baseQuery.ProjectTo<FixtureDto>(_mapper.ConfigurationProvider), query.PaginatedRequestData, order, reverseOrder);
    }
}