using AvaCrm.Domain.Contracts;
using AvaCrm.Domain.Entities;
using AvaCrm.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaCrm.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
	private readonly AvaCrmContext _context;
	public GenericRepository(AvaCrmContext context)
	{
		_context = context;
	}
	public async Task<TEntity> Create(TEntity entity, bool saveNow = true)
	{
		await _context.AddAsync(entity);
		if (saveNow) await _context.SaveChangesAsync();
		return entity;
	}

	public async Task Delete(long entityId)
	{
		var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(e=>e.Id == entityId);
		if (entity!= null)
		{
			entity.IsDelete = true;
			_context.Update(entity);
		}
	}

	public  IQueryable<TEntity> GetAll(bool asNoTracking = true)
	{
		var query = _context.Set<TEntity>().Where(e => !e.IsDelete);
		return asNoTracking ? query.AsNoTracking() : query;
	}

	public async Task<TEntity?> GetById(long entityId, CancellationToken cancellationToken = default)
	{
		return await _context.Set<TEntity>().FindAsync(new object?[] { entityId }, cancellationToken);
	}

	public async Task Update(TEntity entity, CancellationToken cancellationToken = default)
	{
		_context.Set<TEntity>().Update(entity);
		await _context.SaveChangesAsync(cancellationToken);
	}
	
}
