using AutoMapper;
using Sobczal1.KickBets.Application.DTOs.Football.Status;
using Sobczal1.KickBets.Domain.Football;
using FixtureStatus = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Fixtures.Status;

namespace Sobczal1.KickBets.Application.Profiles;

public class StatusProfile : Profile
{
    public StatusProfile()
    {
        CreateMap<FixtureStatus, Status>();
        CreateMap<Status, StatusDto>().ReverseMap();
    }
}