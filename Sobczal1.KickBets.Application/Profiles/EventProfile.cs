using AutoMapper;
using Sobczal1.KickBets.Domain.Football;
using EventRoot = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Events.Root;

namespace Sobczal1.KickBets.Application.Profiles;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<EventRoot, Event>()
            .Include<EventRoot, HomeEvent>()
            .Include<EventRoot, AwayEvent>()
            .ForMember(e => e.ElapsedTime, opt => opt.MapFrom(q => q.Time.Elapsed))
            .ForMember(e => e.ExtraTime, opt => opt.MapFrom(q => q.Time.Extra))
            .ForMember(e => e.TeamId, opt => opt.MapFrom(q => q.Team.Id))
            .ForMember(e => e.Team, opt => opt.Ignore())
            .ForMember(e => e.PlayerName, opt => opt.MapFrom(q => q.Player.Name))
            .ForMember(e => e.AssistName, opt => opt.MapFrom(q => q.Assist.Name));
        CreateMap<EventRoot, HomeEvent>();
        CreateMap<EventRoot, AwayEvent>();
    }
}