using AvaCrm.Domain.Entities.CustomerManagement;

namespace AvaCrm.Domain.Contracts.CustomerManagement;

public interface ITagRepository 
{
	Task<List<Tag>> GetAll();
	Task<Tag?> GetById(int id);
	Task<bool> CreateTag(Tag tag);
	Task<bool> UpdateTag(Tag tag);
	Task<bool> DeleteTag(int id);
}