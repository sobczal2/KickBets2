using Microsoft.EntityFrameworkCore;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.Contracts.Persistence.Bet;
using Sobczal1.KickBets.Application.Contracts.Persistence.Football;
using Sobczal1.KickBets.Persistence.Repositories.Bets;
using Sobczal1.KickBets.Persistence.Repositories.Football;

namespace Sobczal1.KickBets.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly KickBetsDbContext _context;

    public UnitOfWork(KickBetsDbContext context)
    {
        _context = context;
    }

    private IBaseBetRepository? _baseBetRepository;
    private IWdlftBetRepository? _wdlftBetRepository;
    private IWdlhtBetRepository? _wdlhtBetRepository;
    private IEventRepository? _eventRepository;
    private IFixtureRepository? _fixtureRepository;
    private ILeagueRepository? _leagueRepository;
    private ILineupRepository? _lineupRepository;
    private IPlayerRepository? _playerRepository;
    private IScoreRepository? _scoreRepository;
    private IStatisticsRepository? _statisticsRepository;
    private IStatusRepository? _statusRepository;
    private ITeamRepository? _teamRepository;
    private IVenueRepository? _venueRepository;

    public IBaseBetRepository BaseBetRepository => _baseBetRepository ??= new BaseBetRepository(_context);
    public IWdlftBetRepository WdlftBetRepository => _wdlftBetRepository ??= new WdlftBetRepository(_context);
    public IWdlhtBetRepository WdlhtBetRepository => _wdlhtBetRepository ??= new WdlhtBetRepository(_context);
    public IEventRepository EventRepository => _eventRepository ??= new EventRepository(_context);
    public IFixtureRepository FixtureRepository => _fixtureRepository ??= new FixtureRepository(_context);
    public ILeagueRepository LeagueRepository => _leagueRepository ??= new LeagueRepository(_context);
    public ILineupRepository LineupRepository => _lineupRepository ??= new LineupRepository(_context);
    public IPlayerRepository PlayerRepository => _playerRepository ??= new PlayerRepository(_context);
    public IScoreRepository ScoreRepository => _scoreRepository ??= new ScoreRepository(_context);
    public IStatisticsRepository StatisticsRepository => _statisticsRepository ??= new StatisticRepository(_context);
    public IStatusRepository StatusRepository => _statusRepository ??= new StatusRepository(_context);
    public ITeamRepository TeamRepository => _teamRepository ??= new TeamRepository(_context);
    public IVenueRepository VenueRepository => _venueRepository ??= new VenueRepository(_context);
    
    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}