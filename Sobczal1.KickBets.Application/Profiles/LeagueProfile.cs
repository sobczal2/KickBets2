using AutoMapper;
using Sobczal1.KickBets.Application.DTOs.Football.Leagues;
using Sobczal1.KickBets.Domain.Football;
using LeagueRoot = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Leagues.Root;

namespace Sobczal1.KickBets.Application.Profiles;

public class LeagueProfile : Profile
{
    public LeagueProfile()
    {
        CreateMap<LeagueRoot, League>()
            .ForMember(l => l.Id, opt => opt.MapFrom(q => q.League.Id))
            .ForMember(l => l.Name, opt => opt.MapFrom(q => q.League.Name))
            .ForMember(l => l.Country, opt => opt.MapFrom(q => q.Country.Name))
            .ForMember(l => l.Logo, opt => opt.MapFrom(q => q.League.Logo))
            .ForMember(l => l.Flag, opt => opt.MapFrom(q => q.Country.Flag));

        CreateMap<League, LeagueDto>().ReverseMap();
    }
}