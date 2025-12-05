using AvaCrm.Application.DTOs.Commons.Units;
using AvaCrm.Application.Pagination;
using AvaCrm.Domain.Enums.Commons;
using Microsoft.EntityFrameworkCore;

namespace AvaCrm.Application.Features.Commons;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMapper _mapper;

    public UnitService(IUnitRepository unitRepository, IMapper mapper)
    {
        _unitRepository = unitRepository;
        _mapper = mapper;
    }

    public async Task<GlobalResponse<PaginatedResult<UnitDto>>> GetAllUnits(PaginationRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _unitRepository.GetAll();

            // Get total count before pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination
            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var unitDtos = _mapper.Map<List<UnitDto>>(items);

            var paginatedResult = new PaginatedResult<UnitDto>
            {
                Items = unitDtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            return new GlobalResponse<PaginatedResult<UnitDto>>
            {
                StatusCode = 200,
                Message = "واحدها با موفقیت دریافت شدند",
                Data = paginatedResult
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<PaginatedResult<UnitDto>>
            {
                StatusCode = 500,
                Message = $"خطایی در دریافت واحدها رخ داد: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<GlobalResponse<List<UnitDto>>> GetUnitsByCategory(UnitCategory category)
    {
        try
        {
            var units = await _unitRepository.GetUnitsByUnitCategory(category);

            if (units == null || units.Count == 0)
            {
                return new GlobalResponse<List<UnitDto>>
                {
                    StatusCode = 404,
                    Message = $"واحدی برای دسته‌بندی {GetCategoryPersianName(category)} یافت نشد",
                    Data = null
                };
            }

            var unitDtos = _mapper.Map<List<UnitDto>>(units);

            return new GlobalResponse<List<UnitDto>>
            {
                StatusCode = 200,
                Message = $"واحدهای دسته‌بندی {GetCategoryPersianName(category)} با موفقیت دریافت شدند",
                Data = unitDtos
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<List<UnitDto>>
            {
                StatusCode = 500,
                Message = $"خطایی در دریافت واحدهای دسته‌بندی رخ داد: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<GlobalResponse<UnitDto>> GetUnitById(long unitId, CancellationToken cancellationToken = default)
    {
        try
        {
            var unit = await _unitRepository.GetById(unitId, cancellationToken);

            if (unit == null)
            {
                return new GlobalResponse<UnitDto>
                {
                    StatusCode = 404,
                    Message = $"واحد با شناسه {unitId} یافت نشد",
                    Data = null
                };
            }

            var unitDto = _mapper.Map<UnitDto>(unit);

            return new GlobalResponse<UnitDto>
            {
                StatusCode = 200,
                Message = "واحد با موفقیت دریافت شد",
                Data = unitDto
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<UnitDto>
            {
                StatusCode = 500,
                Message = $"خطایی در دریافت واحد رخ داد: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<GlobalResponse<UnitDto>> CreateUnit(CreateUnitDto create, long userId, CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if unit with same code already exists
            var existingUnit = await _unitRepository.GetAll()
                .FirstOrDefaultAsync(u => u.Code == create.Code, cancellationToken);

            if (existingUnit != null)
            {
                return new GlobalResponse<UnitDto>
                {
                    StatusCode = 400,
                    Message = $"واحد با کد {create.Code} قبلاً ثبت شده است",
                    Data = null
                };
            }

            var unit = _mapper.Map<Domain.Entities.Commons.Unit>(create);

            var createdUnit = await _unitRepository.Create(unit, userId);
            var unitDto = _mapper.Map<UnitDto>(createdUnit);

            return new GlobalResponse<UnitDto>
            {
                StatusCode = 201,
                Message = "واحد جدید با موفقیت ایجاد شد",
                Data = unitDto
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<UnitDto>
            {
                StatusCode = 500,
                Message = $"خطایی در ایجاد واحد جدید رخ داد: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<GlobalResponse<UnitDto>> UpdateUnit(UpdateUnitDto update, long userId, CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if unit exists
            var existingUnit = await _unitRepository.GetById(update.Id, cancellationToken);

            if (existingUnit == null)
            {
                return new GlobalResponse<UnitDto>
                {
                    StatusCode = 404,
                    Message = $"واحد با شناسه {update.Id} یافت نشد",
                    Data = null
                };
            }

            // Check if another unit with the same code already exists
            var duplicateUnit = await _unitRepository.GetAll()
                .FirstOrDefaultAsync(u => u.Code == update.Code && u.Id != update.Id, cancellationToken);

            if (duplicateUnit != null)
            {
                return new GlobalResponse<UnitDto>
                {
                    StatusCode = 400,
                    Message = $"واحد دیگری با کد {update.Code} قبلاً ثبت شده است",
                    Data = null
                };
            }

            // Update unit properties
            existingUnit.Name = update.Name;
            existingUnit.Code = update.Code;
            existingUnit.Category = update.Category;

            await _unitRepository.Update(existingUnit, userId, cancellationToken);

            var updatedUnitDto = _mapper.Map<UnitDto>(existingUnit);

            return new GlobalResponse<UnitDto>
            {
                StatusCode = 200,
                Message = "واحد با موفقیت به‌روزرسانی شد",
                Data = updatedUnitDto
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<UnitDto>
            {
                StatusCode = 500,
                Message = $"خطایی در به‌روزرسانی واحد رخ داد: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if unit exists
            var existingUnit = await _unitRepository.GetById(id, cancellationToken);

            if (existingUnit == null)
            {
                return new GlobalResponse<ResponseResultGlobally>
                {
                    StatusCode = 404,
                    Message = $"واحد با شناسه {id} یافت نشد",
                    Data = new ResponseResultGlobally { DoneSuccessfully = false }
                };
            }

            await _unitRepository.Delete(id, userId);

            return new GlobalResponse<ResponseResultGlobally>
            {
                StatusCode = 200,
                Message = "واحد با موفقیت حذف شد",
                Data = new ResponseResultGlobally { DoneSuccessfully = true }
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<ResponseResultGlobally>
            {
                StatusCode = 500,
                Message = $"خطایی در حذف واحد رخ داد: {ex.Message}",
                Data = new ResponseResultGlobally { DoneSuccessfully = false }
            };
        }
    }

    private string GetCategoryPersianName(UnitCategory category)
    {
        return category switch
        {
            UnitCategory.General => "عمومی",
            UnitCategory.Count => "تعداد",
            UnitCategory.Area => "مساحت",
            UnitCategory.Weight => "وزن",
            UnitCategory.Length => "طول",
            UnitCategory.Volume => "حجم",
            UnitCategory.Time => "زمان",

            _ => category.ToString()
        };
    }
}
