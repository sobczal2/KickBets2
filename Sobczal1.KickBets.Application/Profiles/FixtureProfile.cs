using AutoMapper;
using Sobczal1.KickBets.Application.DTOs.Football.Fixtures;
using Sobczal1.KickBets.Domain.Football;
using FixtureRoot = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Fixtures.Root;

namespace Sobczal1.KickBets.Application.Profiles;

public class FixtureProfile : Profile
{
    public FixtureProfile()
    {
        CreateMap<FixtureRoot, Fixture>()
            .ForMember(f => f.Id, opt => opt.MapFrom(q => q.Fixture.Id))
            .ForMember(f => f.Referee, opt => opt.MapFrom(q => q.Fixture.Referee))
            .ForMember(f => f.Date, opt => opt.MapFrom(q => q.Fixture.Date))
            .ForMember(f => f.VenueId, opt => opt.MapFrom(q => q.Fixture.Venue.Id))
            .ForMember(f => f.Status, opt => opt.MapFrom(q => q.Fixture.Status))
            .ForMember(f => f.LeagueId, opt => opt.Ignore())
            .ForMember(f => f.HomeTeam, opt => opt.MapFrom(q => q.Teams.Home))
            .ForMember(f => f.AwayTeam, opt => opt.MapFrom(q => q.Teams.Away))
            .ForMember(f => f.HomeLineup, opt => opt.Ignore())
            .ForMember(f => f.AwayLineup, opt => opt.Ignore())
            .ForMember(f => f.Score, opt => opt.MapFrom(q => ValueTuple.Create(q.Score, q.Goals)))
            .ForMember(f => f.HomeStatistics, opt => opt.Ignore())
            .ForMember(f => f.AwayStatistics, opt => opt.Ignore());


        CreateMap<Fixture, FixtureDto>().ReverseMap();
    }
}