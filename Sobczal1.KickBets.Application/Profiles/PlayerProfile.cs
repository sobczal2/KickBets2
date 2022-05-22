using AutoMapper;
using Sobczal1.KickBets.Application.DTOs.Football.Players;
using Sobczal1.KickBets.Domain.Football;

namespace Sobczal1.KickBets.Application.Profiles;

public class PlayerProfile : Profile
{
    public PlayerProfile()
    {
        CreateMap<Player, PlayerDto>().ReverseMap();
    }
}