namespace AvaCrm.Domain.Contracts;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
	IQueryable<TEntity> GetAll(bool asNoTracking = true);
	Task<TEntity?> GetById(long entityId, CancellationToken cancellationToken = default);
	Task<TEntity> Create(TEntity entity,bool saveNow = true);
	Task Delete(long entityId);
	Task Update(TEntity entity, CancellationToken cancellationToken = default);
	Task<bool> Exists(long entityId, CancellationToken cancellationToken = default);
}
