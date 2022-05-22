using System.Text.Json;
using AutoMapper;
using Sobczal1.KickBets.Application.DTOs.Football.Statistics;
using Sobczal1.KickBets.Domain.Football;
using StatisticsRoot = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Statistics.Root;
using StatisticsApiModel = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Statistics.Statistics;

namespace Sobczal1.KickBets.Application.Profiles;

public class StatisticsProfile : Profile
{
    public StatisticsProfile()
    {
        CreateMap<StatisticsRoot, Statistics>()
            .Include<StatisticsRoot, HomeStatistics>()
            .Include<StatisticsRoot, AwayStatistics>()
            .ForMember(s => s.ShotsOnGoal,
                opt => opt.MapFrom(q => GetInteger(q.Statistics.FirstOrDefault(s => s.Type == "Shots on Goal"))))
            .ForMember(s => s.ShotsOffGoal,
                opt => opt.MapFrom(q => GetInteger(q.Statistics.FirstOrDefault(s => s.Type == "Shots off Goal"))))
            .ForMember(s => s.TotalShots,
                opt => opt.MapFrom(q => GetInteger(q.Statistics.FirstOrDefault(s => s.Type == "Total Shots"))))
            .ForMember(s => s.BlockedShots,
                opt => opt.MapFrom(q => GetInteger(q.Statistics.FirstOrDefault(s => s.Type == "Blocked Shots"))))
            .ForMember(s => s.ShotsInsideBox,
                opt => opt.MapFrom(q => GetInteger(q.Statistics.FirstOrDefault(s => s.Type == "Shots insidebox"))))
            .ForMember(s => s.ShotsOutsideBox,
                opt => opt.MapFrom(q => GetInteger(q.Statistics.FirstOrDefault(s => s.Type == "Shots outsidebox"))))
            .ForMember(s => s.Fouls,
                opt => opt.MapFrom(q => GetInteger(q.Statistics.FirstOrDefault(s => s.Type == "Fouls"))))
            .ForMember(s => s.CornerKicks,
                opt => opt.MapFrom(q => GetInteger(q.Statistics.FirstOrDefault(s => s.Type == "Corner Kicks"))))
            .ForMember(s => s.Offsides,
                opt => opt.MapFrom(q => GetInteger(q.Statistics.FirstOrDefault(s => s.Type == "Offsides"))))
            .ForMember(s => s.YellowCards,
                opt => opt.MapFrom(q => GetInteger(q.Statistics.FirstOrDefault(s => s.Type == "Yellow Cards"))))
            .ForMember(s => s.RedCards,
                opt => opt.MapFrom(q => GetInteger(q.Statistics.FirstOrDefault(s => s.Type == "Red Cards"))))
            .ForMember(s => s.GoalkeeperSaves,
                opt => opt.MapFrom(q => GetInteger(q.Statistics.FirstOrDefault(s => s.Type == "Goalkeeper Saves"))))
            .ForMember(s => s.TotalPasses,
                opt => opt.MapFrom(q => GetInteger(q.Statistics.FirstOrDefault(s => s.Type == "Total passes"))))
            .ForMember(s => s.AccuratePasses,
                opt => opt.MapFrom(q => GetInteger(q.Statistics.FirstOrDefault(s => s.Type == "Passes accurate"))))
            .ForMember(s => s.Possession,
                opt => opt.MapFrom(q => GetDouble(q.Statistics.FirstOrDefault(s => s.Type == "Ball Possession"))))
            .ForMember(s => s.Passes,
            opt => opt.MapFrom(q => GetDouble(q.Statistics.FirstOrDefault(s => s.Type == "Passes %"))));
        CreateMap<StatisticsRoot, HomeStatistics>();
        CreateMap<StatisticsRoot, AwayStatistics>();

        CreateMap<Statistics, StatisticDto>().ReverseMap();
    }

    private int? GetInteger(StatisticsApiModel? statistics)
    {
        if (statistics is null) return null;
        if (statistics.Value is JsonElement element)
        {
            if(element.TryGetInt32(out var value))
                return value;
        }
        return null;
    }
    
    private double? GetDouble(StatisticsApiModel? statistics)
    {
        if (statistics is null) return null;
        if (statistics.Value is JsonElement element)
        {
            var value = element.GetString();
            if (string.IsNullOrEmpty(value))
                return null;
            value = value.Replace("%", "");
            if (double.TryParse(value, out var result))
            {
                return result / 100.0;
            }
        }
        return null;
    }
}