using AvaCrm.Domain.Contracts;

namespace AvaCrm.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AvaCrmContext _context;
    public GenericRepository(AvaCrmContext context)
    {
        _context = context;
    }
    public virtual async Task<TEntity> Create(TEntity entity, long userId, bool saveNow = true)
    {
        entity.CreatedBy = userId;
        entity.ModifiedBy = userId;
        entity.CreationDate = DateTime.Now;
        entity.ModifiedDate = DateTime.Now;
        await _context.AddAsync(entity);
        if (saveNow) await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task Delete(long entityId, long userId)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity != null)
        {
            entity.IsDelete = true;
            entity.ModifiedBy = userId;
            entity.ModifiedDate = DateTime.Now;
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }

    public IQueryable<TEntity> GetAll(bool asNoTracking = true)
    {
        var query = _context.Set<TEntity>().Where(e => !e.IsDelete);
        return asNoTracking ? query.AsNoTracking() : query;
    }

    public async Task<TEntity?> GetById(long entityId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().FindAsync(new object?[] { entityId }, cancellationToken);
    }

    public virtual async Task Update(TEntity entity, long userId, CancellationToken cancellationToken = default)
    {
        entity.ModifiedBy = userId;
        entity.ModifiedDate = DateTime.Now;
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

}
