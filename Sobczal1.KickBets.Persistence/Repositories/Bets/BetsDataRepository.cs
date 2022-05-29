using Microsoft.EntityFrameworkCore;
using Sobczal1.KickBets.Application.Contracts.Persistence.Bet;
using Sobczal1.KickBets.Domain.Bets;

namespace Sobczal1.KickBets.Persistence.Repositories.Bets;

public class BetsDataRepository : IBetsDataRepository
{
    private readonly KickBetsDbContext _dbContext;

    public BetsDataRepository(KickBetsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<BetsData?> Get(int id)
    {
        return await _dbContext.BetsData
            .Include(bd => bd.WdlhtBetsData)
            .Include(bd => bd.WdlftBetsData)
            .FirstOrDefaultAsync(q => q.Id == id);
    }
}