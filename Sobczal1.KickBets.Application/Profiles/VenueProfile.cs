using AutoMapper;
using Sobczal1.KickBets.Application.DTOs.Football.Venues;
using Sobczal1.KickBets.Domain.Football;
using VenueRoot = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Venues.Root;

namespace Sobczal1.KickBets.Application.Profiles;

public class VenueProfile : Profile
{
    public VenueProfile()
    {
        CreateMap<VenueRoot, Venue>();
        CreateMap<Venue, VenueDto>();
    }
}