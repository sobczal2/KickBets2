using AutoMapper;
using Sobczal1.KickBets.Application.DTOs.Football.Lineups;
using Sobczal1.KickBets.Domain.Football;

namespace Sobczal1.KickBets.Application.Profiles;

using LineupRoot = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Lineups.Root;
using PlayerRoot = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Lineups.Player;

public class LineupProfile : Profile
{
    public LineupProfile()
    {
        CreateMap<LineupRoot, Lineup>()
            .Include<LineupRoot, HomeLineup>()
            .Include<LineupRoot, AwayLineup>()
            .ForMember(l => l.CoachName, opt => opt.MapFrom(q => q.Coach.Name))
            .ForMember(l => l.CoachPhoto, opt => opt.MapFrom(q => q.Coach.Photo))
            .ForMember(l => l.Team, opt => opt.Ignore())
            .ForMember(l => l.TeamId, opt => opt.MapFrom(q => q.Team.Id));

        CreateMap<LineupRoot, HomeLineup>();
        CreateMap<LineupRoot, AwayLineup>();

        CreateMap<PlayerRoot, Player>()
            .ForMember(p => p.GridX, opt => opt.MapFrom(q => ConvertPlayerGrid(q.Grid).x))
            .ForMember(p => p.GridY, opt => opt.MapFrom(q => ConvertPlayerGrid(q.Grid).y));

        CreateMap<Lineup, LineupDto>().ReverseMap();
    }

    private (int? x, int? y) ConvertPlayerGrid(string playerGrid)
    {
        if (string.IsNullOrEmpty(playerGrid)) return (null, null);
        var parts = playerGrid.Split(":");
        if (parts.Length != 2) return (null, null);
        if (!int.TryParse(parts[0], out var valueX)) return (null, null);
        if (!int.TryParse(parts[1], out var valueY)) return (null, null);
        return (valueX, valueY);
    }
}