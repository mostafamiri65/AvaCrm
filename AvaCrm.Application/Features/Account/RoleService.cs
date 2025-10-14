using AvaCrm.Application.DTOs.Accounts;
using AvaCrm.Domain.Entities.Accounts;

namespace AvaCrm.Application.Features.Account
{
    public class RoleService : IRoleService
    {
		private readonly IRoleRepository _roleRepository;

		public RoleService(IRoleRepository roleRepository)
		{
			_roleRepository = roleRepository;
		}

		public async Task<GlobalResponse<RoleListDto>> GetByIdAsync(long id, CancellationToken cancellationToken = default)
		{
			try
			{
				var role = await _roleRepository.GetByIdAsync(id, cancellationToken);
				if (role == null)
				{
					return new GlobalResponse<RoleListDto>
					{
						StatusCode = 404,
						Message = "نقش مورد نظر یافت نشد"
					};
				}

				var roleDto = new RoleListDto
				{
					Id = role.Id,
					TitleEnglish = role.TitleEnglish,
					TitlePersian = role.TitlePersian,
					CreatedDate = role.CreationDate
				};

				return new GlobalResponse<RoleListDto>
				{
					StatusCode = 200,
					Message = "نقش با موفقیت دریافت شد",
					Data = roleDto
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<RoleListDto>
				{
					StatusCode = 500,
					Message = $"خطا در دریافت نقش: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<List<RoleListDto>>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			try
			{
				var roles = await _roleRepository.GetAllAsync(cancellationToken);
				return new GlobalResponse<List<RoleListDto>>
				{
					StatusCode = 200,
					Message = "لیست نقش‌ها با موفقیت دریافت شد",
					Data = roles.Select(r=>new RoleListDto()
                    {
                        CreatedDate = r.CreatedDate,
                        Id = r.Id,
                        TitleEnglish = r.TitleEnglish,
                        TitlePersian = r.TitlePersian
                    }).ToList()
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<List<RoleListDto>>
				{
					StatusCode = 500,
					Message = $"خطا در دریافت لیست نقش‌ها: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<RoleListDto>> CreateAsync(RoleCreateDto createDto, CancellationToken cancellationToken = default)
		{
			try
			{
				// بررسی تکراری نبودن عنوان فارسی
				if (await _roleRepository.IsTitlePersianDuplicateAsync(createDto.TitlePersian!, null, cancellationToken))
				{
					return new GlobalResponse<RoleListDto>
					{
						StatusCode = 400,
						Message = "عنوان فارسی نقش تکراری است"
					};
				}

				var role = new Role
				{
					TitleEnglish = createDto.TitleEnglish,
					TitlePersian = createDto.TitlePersian,
				};

				var createdRole = await _roleRepository.CreateAsync(role, cancellationToken);

				var roleDto = new RoleListDto
				{
					Id = createdRole.Id,
					TitleEnglish = createdRole.TitleEnglish,
					TitlePersian = createdRole.TitlePersian,
					CreatedDate = createdRole.CreationDate
				};

				return new GlobalResponse<RoleListDto>
				{
					StatusCode = 201,
					Message = "نقش با موفقیت ایجاد شد",
					Data = roleDto
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<RoleListDto>
				{
					StatusCode = 500,
					Message = $"خطا در ایجاد نقش: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<RoleListDto>> UpdateAsync(RoleUpdateDto updateDto, CancellationToken cancellationToken = default)
		{
			try
			{
				var role = await _roleRepository.GetByIdAsync(updateDto.Id, cancellationToken);
				if (role == null)
				{
					return new GlobalResponse<RoleListDto>
					{
						StatusCode = 404,
						Message = "نقش مورد نظر یافت نشد"
					};
				}

				// بررسی تکراری نبودن عنوان فارسی
				if (await _roleRepository.IsTitlePersianDuplicateAsync(updateDto.TitlePersian!, updateDto.Id, cancellationToken))
				{
					return new GlobalResponse<RoleListDto>
					{
						StatusCode = 400,
						Message = "عنوان فارسی نقش تکراری است"
					};
				}

				role.TitleEnglish = updateDto.TitleEnglish;
				role.TitlePersian = updateDto.TitlePersian;

				var updatedRole = await _roleRepository.UpdateAsync(role, cancellationToken);

				var roleDto = new RoleListDto
				{
					Id = updatedRole.Id,
					TitleEnglish = updatedRole.TitleEnglish,
					TitlePersian = updatedRole.TitlePersian,
					CreatedDate = updatedRole.CreationDate
				};

				return new GlobalResponse<RoleListDto>
				{
					StatusCode = 200,
					Message = "نقش با موفقیت بروزرسانی شد",
					Data = roleDto
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<RoleListDto>
				{
					StatusCode = 500,
					Message = $"خطا در بروزرسانی نقش: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<ResponseResultGlobally>> DeleteAsync(long id, CancellationToken cancellationToken = default)
		{
			try
			{
				var result = await _roleRepository.DeleteAsync(id, cancellationToken);
				if (!result)
				{
					return new GlobalResponse<ResponseResultGlobally>
					{
						StatusCode = 404,
						Message = "نقش مورد نظر یافت نشد"
					};
				}

				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 200,
					Message = "نقش با موفقیت حذف شد",
					Data = new ResponseResultGlobally { DoneSuccessfully = true }
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 500,
					Message = $"خطا در حذف نقش: {ex.Message}"
				};
			}
		}
	}
}