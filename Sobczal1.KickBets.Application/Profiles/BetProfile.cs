using AutoMapper;
using Sobczal1.KickBets.Application.DTOs.Bets;
using Sobczal1.KickBets.Domain.Bets;

namespace Sobczal1.KickBets.Application.Profiles;

public class BetProfile : Profile
{
    public BetProfile()
    {
        CreateMap<BaseBet, BaseBetDto>()
            .ForMember(b => b.Type, opt => opt.MapFrom(q => q.GetBetType()))
            .ForMember(b => b.BetsDataId, opt => opt.MapFrom(q => q.Fixture.BetsDataId));
        CreateMap<BetsData, BetsDataDto>();
        CreateMap<WdlhtBetsData, WdlhtBetsDataDto>()
            .ForMember(bd => bd.HomeBetsMultiplier, opt => opt.MapFrom(q => q.GetHomeBetsMultiplier()))
            .ForMember(bd => bd.DrawBetsMultiplier, opt => opt.MapFrom(q => q.GetDrawBetsMultiplier()))
            .ForMember(bd => bd.AwayBetsMultiplier, opt => opt.MapFrom(q => q.GetAwayBetsMultiplier()));
        CreateMap<WdlftBetsData, WdlftBetsDataDto>()
            .ForMember(bd => bd.HomeBetsMultiplier, opt => opt.MapFrom(q => q.GetHomeBetsMultiplier()))
            .ForMember(bd => bd.DrawBetsMultiplier, opt => opt.MapFrom(q => q.GetDrawBetsMultiplier()))
            .ForMember(bd => bd.AwayBetsMultiplier, opt => opt.MapFrom(q => q.GetAwayBetsMultiplier()));
    }
}