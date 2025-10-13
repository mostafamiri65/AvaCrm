using System.Diagnostics.CodeAnalysis;
using AvaCrm.Domain.Contracts.CustomerManagement;

namespace AvaCrm.Persistence.Repositories.CustomerManagement;

public class TagRepository: ITagRepository
{
	private readonly AvaCrmContext _context;

	public TagRepository(AvaCrmContext context)
	{
		_context = context;
	}
	public async Task<List<Tag>> GetAll()
	{
		return await _context.Tags.ToListAsync();
	}

	public async Task<Tag?> GetById(int id)
	{
		return await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);
	}

	public async Task<bool> CreateTag(Tag tag)
	{
		if (await _context.Tags.AnyAsync(t=>t.Title == tag.Title))
		{
			return false;
		}
		await _context.Tags.AddAsync(tag);
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> UpdateTag(Tag tag)
	{
		if (await _context.Tags.AnyAsync(t => t.Title == tag.Title && t.Id != tag.Id))
		{
			return false;
		}
		var data = await GetById(tag.Id);
		if (data == null) return false;
		data.Title = tag.Title;
		_context.Tags.Update(data);
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> DeleteTag(int id)
	{
		var tag = await GetById(id);
		if (tag == null) return false;
		_context.Tags.Remove(tag);
		await _context.SaveChangesAsync();
		return true;
	}
}