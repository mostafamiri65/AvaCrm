using AvaCrm.Application.DTOs.CustomerManagement.Tags;

namespace AvaCrm.Application.Features.CustomerManagement.Tags;

    public interface ITagService
    {
        Task<List<TagDto>> GetAllTags();
        Task<TagDto> GetById(int id);
        Task<GlobalResponse<TagDto>> CreateTag(string title);
        Task<GlobalResponse<TagDto>> UpdateTag(TagDto tag);
        Task<GlobalResponse<ResponseResultGlobally>> DeleteTag(int id);
    }
