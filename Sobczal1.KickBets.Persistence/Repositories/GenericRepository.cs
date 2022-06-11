using Microsoft.EntityFrameworkCore;
using Sobczal1.KickBets.Application.Contracts.Persistence;

namespace Sobczal1.KickBets.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly KickBetsDbContext _dbContext;

    public GenericRepository(KickBetsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T?> Get(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

#pragma warning disable CS1998
    public async Task<IQueryable<T>> GetAll()
#pragma warning restore CS1998
    {
        return _dbContext.Set<T>();
    }

    public async Task<T> Add(T entity)
    {
        await _dbContext.AddAsync(entity);
        return entity;
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await Get(id);
        return entity != null;
    }

#pragma warning disable CS1998
    public async Task Update(T entity)
#pragma warning restore CS1998
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

#pragma warning disable CS1998
    public async Task Delete(T entity)
#pragma warning restore CS1998
    {
        _dbContext.Set<T>().Remove(entity);
    }
}