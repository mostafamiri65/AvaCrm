using System.Net;
using AvaCrm.Application.DTOs.CustomerManagement.Tags;
using AvaCrm.Domain.Contracts.CustomerManagement;
using AvaCrm.Domain.Entities.CustomerManagement;

namespace AvaCrm.Application.Features.CustomerManagement.Tags
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }
        public async Task<List<TagDto>> GetAllTags()
        {
            var list = await _tagRepository.GetAll();
            return _mapper.Map<List<TagDto>>(list);
        }

        public async Task<TagDto> GetById(int id)
        {
            var tag = await _tagRepository.GetById(id);
            if (tag==null)
            {
                return new TagDto();
            }
            return new TagDto()
            {
                Title = tag.Title,
                Id = tag.Id
            };
        }

        public async Task<GlobalResponse<TagDto>> CreateTag(string title)
        {
            var tag = new Tag()
            {
                Title = title
            };
            if (await _tagRepository.CreateTag(tag))
            {
                return new GlobalResponse<TagDto>()
                {
                    Message = "تگ مورد نظر با موفقیت ایجاد شد",
                    Data = null,
                    StatusCode = 200
                };
            }
            return new GlobalResponse<TagDto>()
                {
                    Message = "تگ مورد نظر موجود است",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Data = null
                };
        }

        public async Task<GlobalResponse<TagDto>> UpdateTag(TagDto tag)
        {
            var entity = await _tagRepository.GetById(tag.Id);
            if (entity==null)
            {
                return new GlobalResponse<TagDto>()
                {
                    Message = "تگ مورد نظر موجود نیست",
                    Data = null,
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            entity.Title = tag.Title;
            await _tagRepository.UpdateTag(entity);
            return new GlobalResponse<TagDto>()
            {
                Message = "ویرایش با موفقیت انجام شد",
                Data = null,
                StatusCode = 200
            };
        }

        public async Task<GlobalResponse<ResponseResultGlobally>> DeleteTag(int id)
        {
            var delete = await _tagRepository.DeleteTag(id);
            if (delete)
            {
				return new GlobalResponse<ResponseResultGlobally>()
                {
                    Message = "حذف با موفقیت انجام شد",
                    Data = new ResponseResultGlobally(){DoneSuccessfully = true},
                    StatusCode = 200
                };
			}

            return new GlobalResponse<ResponseResultGlobally>()
            {
                Message = "حذف با موفقیت انجام نشد",
                Data = new ResponseResultGlobally() { DoneSuccessfully = false },
                StatusCode = (int)HttpStatusCode.NotFound
			};
		}
    }
}