using AvaCrm.Domain.Contracts.ProjectManagement;
using AvaCrm.Domain.Entities.ProjectManagement;

namespace AvaCrm.Persistence.Repositories.ProjectManagement;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    private readonly AvaCrmContext _context;
    public CommentRepository(AvaCrmContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetCommentsByTaskId(long taskId)
    {
        return await _context.Comments.Where(c=>
        c.TaskId == taskId).ToListAsync();
    }
}
