namespace AvaCrm.Domain.Contracts;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
	IQueryable<TEntity> GetAll(bool asNoTracking = true);
	Task<TEntity?> GetById(long entityId, CancellationToken cancellationToken = default);
	Task<TEntity> Create(TEntity entity,long userId,bool saveNow = true);
	Task Delete(long entityId,long userId);
	Task Update(TEntity entity,long userId, CancellationToken cancellationToken = default);
}
