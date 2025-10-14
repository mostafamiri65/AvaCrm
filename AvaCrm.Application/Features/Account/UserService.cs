using AvaCrm.Application.DTOs.Accounts;
using AvaCrm.Domain.Entities.Accounts;

namespace AvaCrm.Application.Features.Account
{
    public class UserService : IUserService
    {
		private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IHashingService _hashingService;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IHashingService hashingService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _hashingService = hashingService;
        }

		public async Task<GlobalResponse<UserListDto>> GetByIdAsync(long id, CancellationToken cancellationToken = default)
		{
			try
			{
				var user = await _userRepository.GetByIdAsync(id, cancellationToken);
				if (user == null)
				{
					return new GlobalResponse<UserListDto>
					{
						StatusCode = 404,
						Message = "کاربر مورد نظر یافت نشد"
					};
				}

				var userDto = MapToUserListDto(user);
				return new GlobalResponse<UserListDto>
				{
					StatusCode = 200,
					Message = "کاربر با موفقیت دریافت شد",
					Data = userDto
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<UserListDto>
				{
					StatusCode = 500,
					Message = $"خطا در دریافت کاربر: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<UserDetailDto>> GetDetailByIdAsync(long id, CancellationToken cancellationToken = default)
		{
			try
			{
				var userDetail = await _userRepository.GetDetailByIdAsync(id, cancellationToken);
				if (userDetail == null)
				{
					return new GlobalResponse<UserDetailDto>
					{
						StatusCode = 404,
						Message = "کاربر مورد نظر یافت نشد"
					};
				}

				return new GlobalResponse<UserDetailDto>
				{
					StatusCode = 200,
					Message = "جزئیات کاربر با موفقیت دریافت شد",
					Data = new UserDetailDto()
                    {
                        Id = userDetail.Id,
                        CreatedDate = userDetail.CreatedDate,
                        RoleId = userDetail.RoleId,
                        AccessFailedCount = userDetail.AccessFailedCount,
                        Email = userDetail.Email,
                        EmailConfirmed = userDetail.EmailConfirmed,
                        LockoutEnabled = userDetail.LockoutEnabled,
                        LockoutEnd = userDetail.LockoutEnd,
                        LockoutTotal = userDetail.LockoutTotal,
                        PhoneNumber = userDetail.PhoneNumber,
                        PhoneNumberConfirmed = userDetail.PhoneNumberConfirmed,
                        RoleTitleEnglish = userDetail.RoleTitleEnglish,
                        RoleTitlePersian = userDetail.RoleTitlePersian,
                        TwoFactorEnabled = userDetail.TwoFactorEnabled,
                        Username = userDetail.Username
                    }
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<UserDetailDto>
				{
					StatusCode = 500,
					Message = $"خطا در دریافت جزئیات کاربر: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<List<UserListDto>>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			try
			{
				var users = await _userRepository.GetAllAsync(cancellationToken);
				return new GlobalResponse<List<UserListDto>>
				{
					StatusCode = 200,
					Message = "لیست کاربران با موفقیت دریافت شد",
					Data = users.Select(u=>new UserListDto()
                    {
						Id = u.Id,
                        CreatedDate = u.CreatedDate,
                        RoleId = u.RoleId,
                        Email = u.Email,
                        EmailConfirmed = u.EmailConfirmed,
                        LockoutEnabled = u.LockoutEnabled,
                        LockoutTotal = u.LockoutTotal,
                        PhoneNumber = u.PhoneNumber,
                        PhoneNumberConfirmed = u.PhoneNumberConfirmed,
                        RoleTitleEnglish = u.RoleTitleEnglish,
                        RoleTitlePersian = u.RoleTitlePersian,
                        Username = u.Username
					}).ToList()
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<List<UserListDto>>
				{
					StatusCode = 500,
					Message = $"خطا در دریافت لیست کاربران: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<UserListDto>> CreateAsync(UserCreateDto createDto, CancellationToken cancellationToken = default)
		{
			try
			{
				// بررسی وجود نقش
				var role = await _roleRepository.GetByIdAsync(createDto.RoleId, cancellationToken);
				if (role == null)
				{
					return new GlobalResponse<UserListDto>
					{
						StatusCode = 400,
						Message = "نقش انتخاب شده معتبر نیست"
					};
				}

				// بررسی تکراری نبودن نام کاربری
				if (await _userRepository.IsUsernameDuplicateAsync(createDto.Username!, null, cancellationToken))
				{
					return new GlobalResponse<UserListDto>
					{
						StatusCode = 400,
						Message = "نام کاربری تکراری است"
					};
				}

				// بررسی تکراری نبودن ایمیل
				if (await _userRepository.IsEmailDuplicateAsync(createDto.Email!, null, cancellationToken))
				{
					return new GlobalResponse<UserListDto>
					{
						StatusCode = 400,
						Message = "ایمیل تکراری است"
					};
				}

				var user = new User
				{
					Username = createDto.Username,
					Email = createDto.Email,
					RoleId = createDto.RoleId,
					PhoneNumber = createDto.PhoneNumber,
					EmailConfirmed = false,
					PhoneNumberConfirmed = false,
					LockoutEnabled = true,
					LockoutTotal = false,
					AccessFailedCount = 0,
					ConcurrencyStamp = Guid.NewGuid().ToString()
				};

				// هش کردن رمز عبور
                user.PasswordHash = _hashingService.Hash(createDto.Password!);

				var createdUser = await _userRepository.CreateAsync(user, cancellationToken);
				var userDto = MapToUserListDto(createdUser);

				return new GlobalResponse<UserListDto>
				{
					StatusCode = 201,
					Message = "کاربر با موفقیت ایجاد شد",
					Data = userDto
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<UserListDto>
				{
					StatusCode = 500,
					Message = $"خطا در ایجاد کاربر: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<UserListDto>> UpdateAsync(UserUpdateDto updateDto, CancellationToken cancellationToken = default)
		{
			try
			{
				var user = await _userRepository.GetByIdAsync(updateDto.Id, cancellationToken);
				if (user == null)
				{
					return new GlobalResponse<UserListDto>
					{
						StatusCode = 404,
						Message = "کاربر مورد نظر یافت نشد"
					};
				}

				// بررسی وجود نقش
				var role = await _roleRepository.GetByIdAsync(updateDto.RoleId, cancellationToken);
				if (role == null)
				{
					return new GlobalResponse<UserListDto>
					{
						StatusCode = 400,
						Message = "نقش انتخاب شده معتبر نیست"
					};
				}

				// بررسی تکراری نبودن نام کاربری
				if (await _userRepository.IsUsernameDuplicateAsync(updateDto.Username!, updateDto.Id, cancellationToken))
				{
					return new GlobalResponse<UserListDto>
					{
						StatusCode = 400,
						Message = "نام کاربری تکراری است"
					};
				}

				// بررسی تکراری نبودن ایمیل
				if (await _userRepository.IsEmailDuplicateAsync(updateDto.Email!, updateDto.Id, cancellationToken))
				{
					return new GlobalResponse<UserListDto>
					{
						StatusCode = 400,
						Message = "ایمیل تکراری است"
					};
				}

				user.Username = updateDto.Username;
				user.Email = updateDto.Email;
				user.RoleId = updateDto.RoleId;
				user.PhoneNumber = updateDto.PhoneNumber;
				user.LockoutEnabled = updateDto.LockoutEnabled;
				user.LockoutTotal = updateDto.LockoutTotal;

				var updatedUser = await _userRepository.UpdateAsync(user, cancellationToken);
				var userDto = MapToUserListDto(updatedUser);

				return new GlobalResponse<UserListDto>
				{
					StatusCode = 200,
					Message = "کاربر با موفقیت بروزرسانی شد",
					Data = userDto
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<UserListDto>
				{
					StatusCode = 500,
					Message = $"خطا در بروزرسانی کاربر: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<ResponseResultGlobally>> DeleteAsync(long id, CancellationToken cancellationToken = default)
		{
			try
			{
				var result = await _userRepository.DeleteAsync(id, cancellationToken);
				if (!result)
				{
					return new GlobalResponse<ResponseResultGlobally>
					{
						StatusCode = 404,
						Message = "کاربر مورد نظر یافت نشد"
					};
				}

				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 200,
					Message = "کاربر با موفقیت حذف شد",
					Data = new ResponseResultGlobally { DoneSuccessfully = true }
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 500,
					Message = $"خطا در حذف کاربر: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<ResponseResultGlobally>> ChangePasswordAsync(UserChangePasswordDto changePasswordDto, CancellationToken cancellationToken = default)
		{
			try
			{
				var user = await _userRepository.GetByIdAsync(changePasswordDto.UserId, cancellationToken);
				if (user == null)
				{
					return new GlobalResponse<ResponseResultGlobally>
					{
						StatusCode = 404,
						Message = "کاربر مورد نظر یافت نشد"
					};
				}

				user.PasswordHash = _hashingService.Hash(changePasswordDto.NewPassword!);
				await _userRepository.UpdateAsync(user, cancellationToken);

				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 200,
					Message = "رمز عبور با موفقیت تغییر یافت",
					Data = new ResponseResultGlobally { DoneSuccessfully = true }
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 500,
					Message = $"خطا در تغییر رمز عبور: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<ResponseResultGlobally>> ToggleLockoutAsync(long id, CancellationToken cancellationToken = default)
		{
			try
			{
				var user = await _userRepository.GetByIdAsync(id, cancellationToken);
				if (user == null)
				{
					return new GlobalResponse<ResponseResultGlobally>
					{
						StatusCode = 404,
						Message = "کاربر مورد نظر یافت نشد"
					};
				}

				user.LockoutTotal = !user.LockoutTotal;
				if (user.LockoutTotal)
				{
					user.LockoutEnd = DateTimeOffset.MaxValue;
				}
				else
				{
					user.LockoutEnd = null;
					user.AccessFailedCount = 0;
				}

				await _userRepository.UpdateAsync(user, cancellationToken);

				var status = user.LockoutTotal ? "قفل شد" : "آزاد شد";
				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 200,
					Message = $"کاربر با موفقیت {status}",
					Data = new ResponseResultGlobally { DoneSuccessfully = true }
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 500,
					Message = $"خطا در تغییر وضعیت قفل کاربر: {ex.Message}"
				};
			}
		}

		private UserListDto MapToUserListDto(User user)
		{
			return new UserListDto
			{
				Id = user.Id,
				Username = user.Username,
				Email = user.Email,
				RoleId = user.RoleId,
				RoleTitlePersian = user.Role.TitlePersian,
				RoleTitleEnglish = user.Role.TitleEnglish,
				EmailConfirmed = user.EmailConfirmed,
				PhoneNumber = user.PhoneNumber,
				PhoneNumberConfirmed = user.PhoneNumberConfirmed,
				LockoutEnabled = user.LockoutEnabled,
				LockoutTotal = user.LockoutTotal,
				CreatedDate = user.CreationDate
			};
		}
	}
}