using System.Net;
using AvaCrm.Application.Features.CustomerManagement.Tags;
using AvaCrm.Domain.Contracts.CustomerManagement;
using AvaCrm.Domain.Entities.CustomerManagement;

namespace AvaCrm.Application.DTOs.CustomerManagement.Tags;

public class TagService : ITagService
{
	private ITagRepository _repository;

	public TagService(ITagRepository repository)
	{
		_repository = repository;
	}
	public async Task<List<TagDto>> GetAllTags()
	{
		var list = await _repository.GetAll();
		return list.Select(t => new TagDto()
		{ Id = t.Id, Title = t.Title })
			.ToList();
	}

	public async Task<TagDto> GetById(int id)
	{
		var tag = await _repository.GetById(id);
		if (tag == null) return new TagDto();
		return new TagDto()
		{
			Id = tag.Id,
			Title = tag.Title
		};
	}

	public async Task<GlobalResponse<TagDto>> CreateTag(string title)
	{
		var tag = new Tag()
		{
			Title = title
		};
		var add = await _repository.CreateTag(tag);
		if (add)
		{
			return new GlobalResponse<TagDto>()
			{
				Data = null,
				Message = "تگ مورد نظر اضافه شد",
				StatusCode = (int)HttpStatusCode.OK
			};
		}
		return new GlobalResponse<TagDto>()
		{
			Data = null,
			Message = "تگ مورد نظر موجود است",
			StatusCode = (int)HttpStatusCode.BadRequest
		};
	}

	public async Task<GlobalResponse<TagDto>> UpdateTag(TagDto tag)
	{
		var tagEntity = await _repository.GetById(tag.Id);
		if (tagEntity == null)
        {
            return new GlobalResponse<TagDto>()
            {
                Data = null,
                Message = "تگ مورد نظر وجود ندارد",
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }

        tagEntity.Title = tag.Title;
        var update =await _repository.UpdateTag(tagEntity);
        if (update)
        {
            return new GlobalResponse<TagDto>()
            {
                Data = tag,
                Message = "ویرایش با موفقیت انجام شد",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        return new GlobalResponse<TagDto>()
        {
            Message = "عنوان وارد شده تکراری است",
            StatusCode = (int)HttpStatusCode.BadRequest
        };
    }

	public async Task<GlobalResponse<ResponseResultGlobally>> DeleteTag(int id)
    {
        var delete = await _repository.DeleteTag(id);
        if (delete)
        {
            return new GlobalResponse<ResponseResultGlobally>()
            {
                Message = "تگ مورد نظر با موفقیت حذف شد",
                Data = new ResponseResultGlobally() { DoneSuccessfully = true },
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        return new GlobalResponse<ResponseResultGlobally>()
        {
            Message = "تگ مورد نظر یافت نشد",
            Data = new ResponseResultGlobally() { DoneSuccessfully = false },
            StatusCode = (int)HttpStatusCode.NotFound
        };
    }
}
