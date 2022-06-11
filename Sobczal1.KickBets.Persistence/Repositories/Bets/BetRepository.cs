using Microsoft.EntityFrameworkCore;
using Sobczal1.KickBets.Application.Contracts.Persistence.Bet;
using Sobczal1.KickBets.Domain.Bets;

namespace Sobczal1.KickBets.Persistence.Repositories.Bets;

public class BetRepository : GenericRepository<BaseBet>, IBetRepository
{
    private readonly KickBetsDbContext _dbContext;

    public BetRepository(KickBetsDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IQueryable<BaseBet>> GetAllWithRelatedData()
    {
        return _dbContext.Bets
            .Include(b => b.AppUser)
            .Include(b => b.Fixture).ThenInclude(f => f.Status)
            .Include(b => b.Fixture).ThenInclude(f => f.Score)
            .Include(b => b.Fixture).ThenInclude(f => f.HomeTeam)
            .Include(b => b.Fixture).ThenInclude(f => f.AwayTeam)
            .Include(b => b.Fixture.BetsData).ThenInclude(bd => bd.WdlhtBetsData)
            .Include(b => b.Fixture.BetsData).ThenInclude(bd => bd.WdlftBetsData);
    }
}