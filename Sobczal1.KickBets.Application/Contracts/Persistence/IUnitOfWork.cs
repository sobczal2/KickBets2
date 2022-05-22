﻿using Sobczal1.KickBets.Application.Contracts.Persistence.Bet;
using Sobczal1.KickBets.Application.Contracts.Persistence.Football;

namespace Sobczal1.KickBets.Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable
{
    IBaseBetRepository BaseBetRepository { get; }
    IWdlftBetRepository WdlftBetRepository { get; }
    IWdlhtBetRepository WdlhtBetRepository { get; }
    IEventRepository EventRepository { get; }
    IFixtureRepository FixtureRepository { get; }
    ILeagueRepository LeagueRepository { get; }
    ILineupRepository LineupRepository { get; }
    IPlayerRepository PlayerRepository { get; }
    IScoreRepository ScoreRepository { get; }
    IStatisticsRepository StatisticsRepository { get; }
    IStatusRepository StatusRepository { get; }
    ITeamRepository TeamRepository { get; }
    IVenueRepository VenueRepository { get; }
    Task Save();
}