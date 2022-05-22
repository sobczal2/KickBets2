using AutoMapper;
using Sobczal1.KickBets.Application.DTOs.Identity;
using Sobczal1.KickBets.Domain.Identity;

namespace Sobczal1.KickBets.Application.Profiles;

public class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap<AppUser, UserDto>()
            .ForMember(u => u.BalanceAddAvailableAt, opt => opt.MapFrom(q => q.BalanceLastAddedAt.AddDays(1)));
    }
}