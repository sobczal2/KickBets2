using AutoMapper;
using Sobczal1.KickBets.Application.DTOs.Football.Team;
using Sobczal1.KickBets.Domain.Football;
using TeamRoot = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Teams.Root;

namespace Sobczal1.KickBets.Application.Profiles;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<TeamRoot, Team>();

        CreateMap<Team, TeamDto>();
    }
}