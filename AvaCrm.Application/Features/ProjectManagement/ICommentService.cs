using AvaCrm.Application.DTOs.ProjectManagement;
using AvaCrm.Application.Pagination;
using AvaCrm.Domain.Entities.ProjectManagement;

namespace AvaCrm.Application.Features.ProjectManagement;

public interface ICommentService
{
    Task<GlobalResponse<PaginatedResult<CommentDto>>> GetCommentsByTaskIdAsync(
        long taskId, int pageNumebr = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<GlobalResponse<CommentDto>> GetCommentByIdAsync(long commentId, CancellationToken cancellationToken = default);
    Task<GlobalResponse<ResponseResultGlobally>> CreateCommentAsync(CreateCommentDto comment, long userId, CancellationToken cancellationToken = default);
    Task<GlobalResponse<ResponseResultGlobally>> DeleteCommentAsync(long commentId, long userId, CancellationToken cancellationToken = default);
    Task<GlobalResponse<ResponseResultGlobally>> UpdateCommentAsync(UpdateCommentDto comment, long userId, CancellationToken cancellationToken = default);
}
