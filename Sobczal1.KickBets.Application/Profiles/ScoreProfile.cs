using AutoMapper;
using Sobczal1.KickBets.Application.DTOs.Football.Scores;
using Sobczal1.KickBets.Domain.Football;
using FixtureGoals = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Fixtures.Goals;
using FixtureScore = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Fixtures.Score;
namespace Sobczal1.KickBets.Application.Profiles;

public class ScoreProfile : Profile
{
    public ScoreProfile()
    {
        CreateMap<(FixtureScore, FixtureGoals), Score>()
            .ForMember(s => s.HomeCurrentScore, opt => opt.MapFrom(q => q.Item2.Home))
            .ForMember(s => s.AwayCurrentScore, opt => opt.MapFrom(q => q.Item2.Away))
            .ForMember(s => s.HomeHalfTime, opt => opt.MapFrom(q => q.Item1.Halftime.Home))
            .ForMember(s => s.HomeFullTime, opt => opt.MapFrom(q => q.Item1.Fulltime.Home))
            .ForMember(s => s.HomeExtraTime, opt => opt.MapFrom(q => q.Item1.Extratime.Home))
            .ForMember(s => s.HomePenalty, opt => opt.MapFrom(q => q.Item1.Penalty.Home))
            .ForMember(s => s.AwayHalfTime, opt => opt.MapFrom(q => q.Item1.Halftime.Away))
            .ForMember(s => s.AwayFullTime, opt => opt.MapFrom(q => q.Item1.Fulltime.Away))
            .ForMember(s => s.AwayExtraTime, opt => opt.MapFrom(q => q.Item1.Extratime.Away))
            .ForMember(s => s.AwayPenalty, opt => opt.MapFrom(q => q.Item1.Penalty.Away));

        CreateMap<Score, ScoreDto>().ReverseMap();
    }
}