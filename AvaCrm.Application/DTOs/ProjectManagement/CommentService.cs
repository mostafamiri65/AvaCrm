using AvaCrm.Application.Features.ProjectManagement;
using AvaCrm.Application.Pagination;
using AvaCrm.Domain.Contracts.ProjectManagement;
using AvaCrm.Domain.Entities.ProjectManagement;

namespace AvaCrm.Application.DTOs.ProjectManagement;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<GlobalResponse<PaginatedResult<CommentDto>>> GetCommentsByTaskIdAsync(
        long taskId, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        try
        {
            var comments = await _commentRepository.GetCommentsByTaskId(taskId);

            // Pagination
            var totalCount = comments.Count;
            var pagedComments = comments
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var commentDtos = _mapper.Map<List<CommentDto>>(pagedComments);

            var paginatedResult = new PaginatedResult<CommentDto>
            {
                Items = commentDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return new GlobalResponse<PaginatedResult<CommentDto>>
            {
                StatusCode = 200,
                Message = "Comments retrieved successfully",
                Data = paginatedResult
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<PaginatedResult<CommentDto>>
            {
                StatusCode = 500,
                Message = $"Error retrieving comments: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<GlobalResponse<CommentDto>> GetCommentByIdAsync(long commentId, CancellationToken cancellationToken = default)
    {
        try
        {
            var comment = await _commentRepository.GetById(commentId, cancellationToken);

            if (comment == null)
            {
                return new GlobalResponse<CommentDto>
                {
                    StatusCode = 404,
                    Message = "Comment not found",
                    Data = null
                };
            }

            var commentDto = _mapper.Map<CommentDto>(comment);

            return new GlobalResponse<CommentDto>
            {
                StatusCode = 200,
                Message = "Comment retrieved successfully",
                Data = commentDto
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<CommentDto>
            {
                StatusCode = 500,
                Message = $"Error retrieving comment: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<GlobalResponse<ResponseResultGlobally>> CreateCommentAsync(
        CreateCommentDto commentDto, long userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var comment = _mapper.Map<Comment>(commentDto);
            var createdComment = await _commentRepository.Create(comment, userId, false);

            return new GlobalResponse<ResponseResultGlobally>
            {
                StatusCode = 201,
                Message = "Comment created successfully",
                Data = null
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<ResponseResultGlobally>
            {
                StatusCode = 500,
                Message = $"Error creating comment: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<GlobalResponse<ResponseResultGlobally>> DeleteCommentAsync(
        long commentId, long userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var existingComment = await _commentRepository.GetById(commentId, cancellationToken);
            if (existingComment == null)
            {
                return new GlobalResponse<ResponseResultGlobally>
                {
                    StatusCode = 404,
                    Message = "Comment not found",
                    Data = null
                };
            }

            await _commentRepository.Delete(commentId, userId);

            return new GlobalResponse<ResponseResultGlobally>
            {
                StatusCode = 200,
                Message = "Comment deleted successfully",
                Data = null
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<ResponseResultGlobally>
            {
                StatusCode = 500,
                Message = $"Error deleting comment: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<GlobalResponse<ResponseResultGlobally>> UpdateCommentAsync(
        UpdateCommentDto commentDto, long userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var existingComment = await _commentRepository.GetById(commentDto.Id, cancellationToken);
            if (existingComment == null)
            {
                return new GlobalResponse<ResponseResultGlobally>
                {
                    StatusCode = 404,
                    Message = "Comment not found",
                    Data = null
                };
            }

            _mapper.Map(commentDto, existingComment);
            await _commentRepository.Update(existingComment, userId, cancellationToken);

            return new GlobalResponse<ResponseResultGlobally>
            {
                StatusCode = 200,
                Message = "Comment updated successfully",
                Data = null
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<ResponseResultGlobally>
            {
                StatusCode = 500,
                Message = $"Error updating comment: {ex.Message}",
                Data = null
            };
        }
    }
}
