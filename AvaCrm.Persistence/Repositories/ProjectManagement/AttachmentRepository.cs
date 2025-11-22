using AvaCrm.Domain.Contracts.ProjectManagement;
using AvaCrm.Domain.Entities.ProjectManagement;

namespace AvaCrm.Persistence.Repositories.ProjectManagement;

public class AttachmentRepository : GenericRepository<Attachment>, IAttachmentRepository
{
    private readonly AvaCrmContext _context;
    public AttachmentRepository(AvaCrmContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Attachment>> GetAttachmentsByTaskId(long taskId)
    {
       return await _context.Attachments.Where(a=>a.TaskId == taskId).ToListAsync();
    }
}
