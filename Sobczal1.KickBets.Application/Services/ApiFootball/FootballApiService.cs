using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using Sobczal1.KickBets.Application.Contracts.Persistence;
using Sobczal1.KickBets.Application.Services.ApiFootball.Models.Common;
using Sobczal1.KickBets.Domain.Bets;
using Sobczal1.KickBets.Domain.Football;
using LeaguesRoot = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Leagues.Root;
using VenueRoot = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Venues.Root;
using FixtureRoot = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Fixtures.Root;
using StatisticsRoot = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Statistics.Root;
using LineupRoot = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Lineups.Root;
using EventRoot = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Events.Root;

namespace Sobczal1.KickBets.Application.Services.ApiFootball;

public class FootballApiService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<FootballApiService> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public FootballApiService(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<FootballApiService> logger,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<bool> UpdateTrackedLeagues()
    {
        var fetched = 0;
        foreach (var leagueId in _configuration.GetSection("ApiFootball:TrackedLeagues").Get<int[]>())
            if (await UpdateLeague(leagueId))
                fetched++;

        await _unitOfWork.Save();

        return fetched > 0;
    }

    public async Task<bool> UpdateFixtures(DateTime from, DateTime to)
    {
        var fetched = 0;
        var trackedLeagues = _configuration.GetSection("ApiFootball:TrackedLeagues").Get<int[]>();
        foreach (var league in trackedLeagues)
            if (await UpdateFixture(league, from, to))
                fetched++;

        await _unitOfWork.Save();

        return fetched > 0;
    }

    public async Task<bool> UpdateStatistics()
    {
        var fetched = 0;
        var ongoingOrFrequentlyEndedFixturesOrNotUpdated = await (await _unitOfWork.FixtureRepository.GetAll())
            .Where(f =>
                (DateTime.Compare(f.Date.AddHours(3), DateTime.Now) > 0 &&
                 DateTime.Compare(f.Date, DateTime.Now) < 0) ||
                (DateTime.Compare(f.Date, DateTime.Now) < 0 &&
                 (f.HomeStatistics == null ||
                  f.AwayStatistics == null))).AsNoTracking().ToListAsync();
        foreach (var fixture in ongoingOrFrequentlyEndedFixturesOrNotUpdated)
            if (await UpdateStatistics(fixture.Id))
                fetched++;

        await _unitOfWork.Save();

        return fetched > 0;
    }

    public async Task<bool> UpdateLineups()
    {
        var fetched = 0;
        var ongoingOrFrequentlyEndedFixturesOrNotUpdated = await (await _unitOfWork.FixtureRepository.GetAll())
            .Where(f =>
                (DateTime.Compare(f.Date.AddHours(1), DateTime.Now) > 0 &&
                 DateTime.Compare(f.Date, DateTime.Now.AddHours(-1)) < 0) ||
                (DateTime.Compare(f.Date, DateTime.Now) < 0 &&
                 (f.HomeLineup == null ||
                  f.AwayLineup == null))).AsNoTracking().ToListAsync();

        foreach (var fixture in ongoingOrFrequentlyEndedFixturesOrNotUpdated)
            if (await UpdateLineups(fixture.Id))
                fetched++;

        await _unitOfWork.Save();

        return fetched > 0;
    }

    public async Task<bool> UpdateEvents()
    {
        var fetched = 0;
        var ongoingOrFrequentlyEndedFixturesOrNotUpdated = await (await _unitOfWork.FixtureRepository.GetAll())
            .Where(f =>
                (DateTime.Compare(f.Date.AddHours(3), DateTime.Now) > 0 &&
                 DateTime.Compare(f.Date, DateTime.Now) < 0) ||
                (DateTime.Compare(f.Date, DateTime.Now) < 0 &&
                 (f.HomeEvents.Count == 0 ||
                  f.AwayEvents.Count == 0))).AsNoTracking().ToListAsync();

        foreach (var fixture in ongoingOrFrequentlyEndedFixturesOrNotUpdated)
            if (await UpdateEvents(fixture.Id))
                fetched++;

        await _unitOfWork.Save();

        return fetched > 0;
    }

    private async Task<bool> UpdateFixture(int leagueId, DateTime from, DateTime to)
    {
        var league = await _unitOfWork.LeagueRepository.Get(leagueId);
        if (league is null) return false;
        var client =
            new RestClient(
                $"https://api-football-v1.p.rapidapi.com/v3/fixtures?league={league.Id}&season={league.Season}&from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}");
        var request = new RestRequest();
        request.AddHeader("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
        request.AddHeader("X-RapidAPI-Key", _configuration["ApiFootball:Key"]);

        var response = await client.GetAsync<Result<FixtureRoot, object>>(request, CancellationToken.None);

        if (response.Errors.Count != 0)
        {
            _logger.LogError($"Error fetching fixture data for league with id: {leagueId}");
            return false;
        }

        if (response.Response.Count == 0) _logger.LogWarning($"No fixtures fetched for league with id: {leagueId}");

        foreach (var fixture in response.Response)
        {
            var fixtureInDb = await _unitOfWork.FixtureRepository.Get(fixture.Fixture.Id);
            if (fixtureInDb != null)
                await UpdateFixtureAlreadyInDb(fixture, fixtureInDb, leagueId);
            else
                await UpdateFixtureNotYetInDb(fixture, leagueId);
        }

        return true;
    }

    private async Task<bool> UpdateFixtureAlreadyInDb(FixtureRoot fixtureRoot, Fixture fixtureInDb, int leagueId)
    {
        _mapper.Map(fixtureRoot, fixtureInDb);
        fixtureInDb.LeagueId = leagueId;
        await FixFixtureOnInsert(fixtureInDb);
        await _unitOfWork.FixtureRepository.Update(fixtureInDb);
        await _unitOfWork.Save();
        return true;
    }

    private async Task<bool> UpdateFixtureNotYetInDb(FixtureRoot fixtureRoot, int leagueId)
    {
        var fixture = _mapper.Map<Fixture>(fixtureRoot);
        fixture.LeagueId = leagueId;
        fixture.BetsData = new BetsData();
        await FixFixtureOnInsert(fixture);
        await _unitOfWork.FixtureRepository.Add(fixture);
        await _unitOfWork.Save();
        return true;
    }

    private async Task FixFixtureOnInsert(Fixture fixture)
    {
        if (fixture.VenueId.HasValue)
            if (!await _unitOfWork.VenueRepository.Exists(fixture.VenueId.Value))
                if (!await UpdateVenue(fixture.VenueId.Value))
                    fixture.VenueId = null;

        var homeTeam = await _unitOfWork.TeamRepository.Get(fixture.HomeTeam.Id);

        if (homeTeam is not null) fixture.HomeTeam = homeTeam;

        var awayTeam = await _unitOfWork.TeamRepository.Get(fixture.AwayTeam.Id);

        if (awayTeam is not null) fixture.AwayTeam = awayTeam;
    }

    private async Task<bool> UpdateLeague(int leagueId)
    {
        try
        {
            var client =
                new RestClient($"https://api-football-v1.p.rapidapi.com/v3/leagues?id={leagueId}&current=true");
            var request = new RestRequest();
            request.AddHeader("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("X-RapidAPI-Key", _configuration["ApiFootball:Key"]);
            var response = await client.GetAsync<Result<LeaguesRoot, object>>(request, CancellationToken.None);

            if (response == null || response.Errors.Count != 0 || response.Response.Count != 1)
            {
                _logger.LogError($"Error fetching league data for id: {leagueId}");
                return false;
            }

            var currentSeason = response.Response[0].Seasons.FirstOrDefault(s => s.Current);
            if (currentSeason == null) return false;

            var leagueInDb = await _unitOfWork.LeagueRepository.Get(leagueId);
            if (leagueInDb != null)
            {
                _mapper.Map(response.Response[0], leagueInDb);
                leagueInDb.Season = currentSeason.Year;
                await _unitOfWork.LeagueRepository.Update(leagueInDb);
            }
            else
            {
                var leagueToAdd = _mapper.Map<League>(response.Response[0]);
                leagueToAdd.Season = currentSeason.Year;
                await _unitOfWork.LeagueRepository.Add(leagueToAdd);
            }

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"Update failed for league with id: {leagueId}");
            return false;
        }
    }

    private async Task<bool> UpdateVenue(int venueId)
    {
        try
        {
            var client = new RestClient($"https://api-football-v1.p.rapidapi.com/v3/venues?id={venueId}");
            var request = new RestRequest();
            request.AddHeader("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("X-RapidAPI-Key", _configuration["ApiFootball:Key"]);

            var response = await client.GetAsync<Result<VenueRoot, object>>(request, CancellationToken.None);

            if (response == null || response.Errors.Count != 0 || response.Response.Count != 1)
            {
                _logger.LogError($"Error fetching venue data for id: {venueId}");
                return false;
            }

            var venueInDb = await _unitOfWork.VenueRepository.Get(venueId);
            if (venueInDb != null)
            {
                _mapper.Map(response.Response[0], venueInDb);
                await _unitOfWork.VenueRepository.Update(venueInDb);
            }
            else
            {
                await _unitOfWork.VenueRepository.Add(_mapper.Map<Venue>(response.Response[0]));
            }

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"Update failed for venue with id: {venueId}");
            return false;
        }
    }

    private async Task<bool> UpdateStatistics(int fixtureId)
    {
        try
        {
            var client =
                new RestClient($"https://api-football-v1.p.rapidapi.com/v3/fixtures/statistics?fixture={fixtureId}");
            var request = new RestRequest();
            request.AddHeader("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("X-RapidAPI-Key", _configuration["ApiFootball:Key"]);

            var response = await client.GetAsync<Result<StatisticsRoot, object>>(request, CancellationToken.None);

            if (response == null || response.Errors.Count != 0)
            {
                _logger.LogError($"Error fetching statistics data for fixture with id: {fixtureId}");
                return false;
            }

            if (response.Response.Count != 2)
            {
                _logger.LogWarning($"Statistics data for fixture with id: {fixtureId} unavailable.");
                return false;
            }

            var fixtureInDb = await _unitOfWork.FixtureRepository.GetWithStatistics(fixtureId);

            if (fixtureInDb is null)
            {
                _logger.LogError($"Error fetching statistics data for fixture with id: {fixtureId}");
                return false;
            }

            var homeTeamId = fixtureInDb.HomeTeamId;

            var homeStatistics = response.Response.FirstOrDefault(s => s.Team.Id == homeTeamId);
            if (homeStatistics is null)
            {
                _logger.LogError($"Error fetching statistics data for fixture with id: {fixtureId}");
                return false;
            }

            if (fixtureInDb.HomeStatistics is null)
            {
                var homeStatisticsToAdd = _mapper.Map<HomeStatistics>(homeStatistics);
                fixtureInDb.HomeStatistics = homeStatisticsToAdd;
                await _unitOfWork.FixtureRepository.Update(fixtureInDb);
            }
            else
            {
                var homeStatisticsInDb = fixtureInDb.HomeStatistics;
                _mapper.Map(homeStatistics, homeStatisticsInDb);
                await _unitOfWork.StatisticsRepository.Update(homeStatisticsInDb);
            }

            var awayTeamId = fixtureInDb.AwayTeamId;

            var awayStatistics = response.Response.FirstOrDefault(s => s.Team.Id == awayTeamId);
            if (awayStatistics is null)
            {
                _logger.LogError($"Error fetching statistics data for fixture with id: {fixtureId}");
                return false;
            }

            if (fixtureInDb.AwayStatistics is null)
            {
                var awayStatisticsToAdd = _mapper.Map<AwayStatistics>(awayStatistics);
                fixtureInDb.AwayStatistics = awayStatisticsToAdd;
                await _unitOfWork.FixtureRepository.Update(fixtureInDb);
            }
            else
            {
                var awayStatisticsInDb = fixtureInDb.AwayStatistics;
                _mapper.Map(awayStatistics, awayStatisticsInDb);
                await _unitOfWork.StatisticsRepository.Update(awayStatisticsInDb);
            }

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"Update failed for statistics of fixture with id: {fixtureId}");
            return false;
        }
    }

    private async Task<bool> UpdateLineups(int fixtureId)
    {
        try
        {
            var client =
                new RestClient($"https://api-football-v1.p.rapidapi.com/v3/fixtures/lineups?fixture={fixtureId}");
            var request = new RestRequest();
            request.AddHeader("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("X-RapidAPI-Key", _configuration["ApiFootball:Key"]);

            var response = await client.GetAsync<Result<LineupRoot, object>>(request, CancellationToken.None);

            if (response == null || response.Errors.Count != 0)
            {
                _logger.LogError($"Error fetching lineups data for fixture with id: {fixtureId}");
                return false;
            }

            if (response.Response.Count != 2)
            {
                _logger.LogWarning($"Lineups data for fixture with id: {fixtureId} unavailable.");
                return false;
            }

            var fixtureInDb = await _unitOfWork.FixtureRepository.GetWithLineups(fixtureId);

            if (fixtureInDb is null)
            {
                _logger.LogError($"Error fetching lineups data for fixture with id: {fixtureId}");
                return false;
            }

            var homeTeamId = fixtureInDb.HomeTeamId;

            var homeLineup = response.Response.FirstOrDefault(s => s.Team.Id == homeTeamId);
            if (homeLineup is null)
            {
                _logger.LogError($"Error fetching lineups data for fixture with id: {fixtureId}");
                return false;
            }

            if (fixtureInDb.HomeLineup is null)
            {
                var homeLineupToAdd = _mapper.Map<HomeLineup>(homeLineup);
                var playersList = new List<Player>();
                playersList.AddRange(homeLineup.StartXI.Select(p =>
                {
                    var player = _mapper.Map<Player>(p.Player);
                    player.Starting11 = true;
                    return player;
                }));
                playersList.AddRange(homeLineup.Substitutes.Select(p =>
                {
                    var player = _mapper.Map<Player>(p.Player);
                    player.Starting11 = false;
                    return player;
                }));
                homeLineupToAdd.Players = playersList;
                fixtureInDb.HomeLineup = homeLineupToAdd;
                await _unitOfWork.FixtureRepository.Update(fixtureInDb);
            }
            else
            {
                var homeLineupInDb = fixtureInDb.HomeLineup;
                _mapper.Map(homeLineup, homeLineupInDb);
                var playersList = new List<Player>();
                playersList.AddRange(homeLineup.StartXI.Select(p =>
                {
                    var player = _mapper.Map<Player>(p.Player);
                    player.Starting11 = true;
                    return player;
                }));
                playersList.AddRange(homeLineup.Substitutes.Select(p =>
                {
                    var player = _mapper.Map<Player>(p.Player);
                    player.Starting11 = false;
                    return player;
                }));
                foreach (var player in homeLineupInDb.Players) await _unitOfWork.PlayerRepository.Delete(player);

                await _unitOfWork.Save();
                homeLineupInDb.Players = playersList;
                await _unitOfWork.LineupRepository.Update(homeLineupInDb);
            }

            var awayTeamId = fixtureInDb.AwayTeamId;

            var awayLineup = response.Response.FirstOrDefault(s => s.Team.Id == awayTeamId);
            if (awayLineup is null)
            {
                _logger.LogError($"Error fetching lineups data for fixture with id: {fixtureId}");
                return false;
            }

            if (fixtureInDb.AwayLineup is null)
            {
                var awayLineupToAdd = _mapper.Map<AwayLineup>(awayLineup);
                var playersList = new List<Player>();
                playersList.AddRange(awayLineup.StartXI.Select(p =>
                {
                    var player = _mapper.Map<Player>(p.Player);
                    player.Starting11 = true;
                    return player;
                }));
                playersList.AddRange(awayLineup.Substitutes.Select(p =>
                {
                    var player = _mapper.Map<Player>(p.Player);
                    player.Starting11 = false;
                    return player;
                }));
                awayLineupToAdd.Players = playersList;
                fixtureInDb.AwayLineup = awayLineupToAdd;
                await _unitOfWork.FixtureRepository.Update(fixtureInDb);
            }
            else
            {
                var awayLineupInDb = fixtureInDb.AwayLineup;
                _mapper.Map(awayLineup, awayLineupInDb);
                var playersList = new List<Player>();
                playersList.AddRange(awayLineup.StartXI.Select(p =>
                {
                    var player = _mapper.Map<Player>(p.Player);
                    player.Starting11 = true;
                    return player;
                }));
                playersList.AddRange(awayLineup.Substitutes.Select(p =>
                {
                    var player = _mapper.Map<Player>(p.Player);
                    player.Starting11 = false;
                    return player;
                }));
                foreach (var player in awayLineupInDb.Players) await _unitOfWork.PlayerRepository.Delete(player);

                await _unitOfWork.Save();
                awayLineupInDb.Players = playersList;
                await _unitOfWork.LineupRepository.Update(awayLineupInDb);
            }

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"Update failed for lineups of fixture with id: {fixtureId}");
            return false;
        }
    }

    private async Task<bool> UpdateEvents(int fixtureId)
    {
        try
        {
            var client =
                new RestClient($"https://api-football-v1.p.rapidapi.com/v3/fixtures/events?fixture={fixtureId}");
            var request = new RestRequest();
            request.AddHeader("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("X-RapidAPI-Key", _configuration["ApiFootball:Key"]);

            var response = await client.GetAsync<Result<EventRoot, object>>(request, CancellationToken.None);

            if (response == null || response.Errors.Count != 0)
            {
                _logger.LogError($"Error fetching events data for fixture with id: {fixtureId}");
                return false;
            }

            if (response.Response.Count == 0)
            {
                _logger.LogWarning($"Events data for fixture with id: {fixtureId} unavailable.");
                return false;
            }

            var fixtureInDb = await _unitOfWork.FixtureRepository.GetWithEvents(fixtureId);

            if (fixtureInDb is null)
            {
                _logger.LogError($"Error fetching events data for fixture with id: {fixtureId}");
                return false;
            }

            var homeTeamId = fixtureInDb.HomeTeamId;

            var homeEvents = response.Response.FirstOrDefault(s => s.Team.Id == homeTeamId);
            if (homeEvents is null)
            {
                _logger.LogError($"Error fetching events data for fixture with id: {fixtureId}");
                return false;
            }

            foreach (var fixtureEvent in fixtureInDb.HomeEvents) await _unitOfWork.EventRepository.Delete(fixtureEvent);

            await _unitOfWork.Save();

            var homeList = new List<HomeEvent>();
            foreach (var fixtureEvent in response.Response.Where(r => r.Team.Id == homeTeamId))
                homeList.Add(_mapper.Map<HomeEvent>(fixtureEvent));

            fixtureInDb.HomeEvents = homeList;

            await _unitOfWork.FixtureRepository.Update(fixtureInDb);

            await _unitOfWork.Save();


            var awayTeamId = fixtureInDb.AwayTeamId;

            var awayEvents = response.Response.FirstOrDefault(s => s.Team.Id == awayTeamId);
            if (awayEvents is null)
            {
                _logger.LogError($"Error fetching events data for fixture with id: {fixtureId}");
                return false;
            }

            foreach (var fixtureEvent in fixtureInDb.AwayEvents) await _unitOfWork.EventRepository.Delete(fixtureEvent);

            await _unitOfWork.Save();

            var awayList = new List<AwayEvent>();
            foreach (var fixtureEvent in response.Response.Where(r => r.Team.Id == awayTeamId))
                awayList.Add(_mapper.Map<AwayEvent>(fixtureEvent));

            fixtureInDb.AwayEvents = awayList;

            await _unitOfWork.FixtureRepository.Update(fixtureInDb);

            await _unitOfWork.Save();

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"Update failed for events of fixture with id: {fixtureId}");
            return false;
        }
    }
}