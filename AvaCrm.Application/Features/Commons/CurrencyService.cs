using AvaCrm.Application.DTOs.Commons.Currencies;
using AvaCrm.Application.Pagination;
using Microsoft.EntityFrameworkCore;

namespace AvaCrm.Application.Features.Commons;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IMapper _mapper;

    public CurrencyService(ICurrencyRepository currencyRepository, IMapper mapper)
    {
        _currencyRepository = currencyRepository;
        _mapper = mapper;
    }

    public async Task<GlobalResponse<PaginatedResult<CurrencyDto>>> GetAllCurrencies(PaginationRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _currencyRepository.GetAll();

            // Get total count before pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination
            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var currencyDtos = _mapper.Map<List<CurrencyDto>>(items);

            var paginatedResult = new PaginatedResult<CurrencyDto>
            {
                Items = currencyDtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            return new GlobalResponse<PaginatedResult<CurrencyDto>>
            {
                StatusCode = 200,
                Message = "لیست واحد پولیها با موفقیت دریافت شد",
                Data = paginatedResult
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<PaginatedResult<CurrencyDto>>
            {
                StatusCode = 500,
                Message = $"خطایی در دریافت لیست واحد پولیها رخ داد: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<GlobalResponse<CurrencyDto>> GetCurrencyById(long currencyId, CancellationToken cancellationToken = default)
    {
        try
        {
            var currency = await _currencyRepository.GetById(currencyId, cancellationToken);

            if (currency == null)
            {
                return new GlobalResponse<CurrencyDto>
                {
                    StatusCode = 404,
                    Message = $"واحد پولی با شناسه {currencyId} یافت نشد",
                    Data = null
                };
            }

            var currencyDto = _mapper.Map<CurrencyDto>(currency);

            return new GlobalResponse<CurrencyDto>
            {
                StatusCode = 200,
                Message = "اطلاعات واحد پولی با موفقیت دریافت شد",
                Data = currencyDto
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<CurrencyDto>
            {
                StatusCode = 500,
                Message = $"خطایی در دریافت اطلاعات واحد پولی رخ داد: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<GlobalResponse<CurrencyDto>> CreateCurrency(CreateCurrencyDto create, long userId, CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if currency with same code already exists
            var existingCurrency = await _currencyRepository.GetAll()
                .FirstOrDefaultAsync(c => c.Code == create.Code, cancellationToken);

            if (existingCurrency != null)
            {
                return new GlobalResponse<CurrencyDto>
                {
                    StatusCode = 400,
                    Message = $"واحد پولی با کد {create.Code} قبلاً ثبت شده است",
                    Data = null
                };
            }

            var currency = _mapper.Map<Currency>(create);

            // Note: منطق مدیریت واحد پولی پیش‌فرض در ریپازیتوری handle می‌شود (متد Create)
            var createdCurrency = await _currencyRepository.Create(currency, userId);
            var currencyDto = _mapper.Map<CurrencyDto>(createdCurrency);

            return new GlobalResponse<CurrencyDto>
            {
                StatusCode = 201,
                Message = "واحد پولی جدید با موفقیت ایجاد شد",
                Data = currencyDto
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<CurrencyDto>
            {
                StatusCode = 500,
                Message = $"خطایی در ایجاد واحد پولی جدید رخ داد: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<GlobalResponse<CurrencyDto>> UpdateCurrency(UpdateCurrencyDto update, long userId, CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if currency exists
            var existingCurrency = await _currencyRepository.GetById(update.Id, cancellationToken);

            if (existingCurrency == null)
            {
                return new GlobalResponse<CurrencyDto>
                {
                    StatusCode = 404,
                    Message = $"واحد پولی با شناسه {update.Id} یافت نشد",
                    Data = null
                };
            }

            // Check if another currency with the same code already exists
            var duplicateCurrency = await _currencyRepository.GetAll()
                .FirstOrDefaultAsync(c => c.Code == update.Code && c.Id != update.Id, cancellationToken);

            if (duplicateCurrency != null)
            {
                return new GlobalResponse<CurrencyDto>
                {
                    StatusCode = 400,
                    Message = $"واحد پولی دیگری با کد {update.Code} قبلاً ثبت شده است",
                    Data = null
                };
            }

            // Update currency properties
            existingCurrency.Code = update.Code;
            existingCurrency.Name = update.Name;
            existingCurrency.Symbol = update.Symbol;
            existingCurrency.DecimalPlaces = update.DecimalPlaces;

            // If changing default status, use ChangeDefaultCurrency method
            if (existingCurrency.IsDefault != update.IsDefault)
            {
                // If trying to set as default
                if (update.IsDefault)
                {
                    await _currencyRepository.ChangeDefaultCurrency(update.Id, true);
                }
                else // Trying to remove default
                {
                    // First check if there's at least one other currency
                    var otherCurrenciesCount = await _currencyRepository.GetAll()
                        .Where(c => c.Id != update.Id)
                        .CountAsync(cancellationToken);

                    if (otherCurrenciesCount == 0)
                    {
                        return new GlobalResponse<CurrencyDto>
                        {
                            StatusCode = 400,
                            Message = "حداقل باید یک واحد پولی در سیستم وجود داشته باشد. امکان حذف پیش‌فرض بودن تنها واحد پولی موجود وجود ندارد.",
                            Data = null
                        };
                    }

                    await _currencyRepository.ChangeDefaultCurrency(update.Id, false);
                }

                // After changing default status, get the updated entity
                existingCurrency = await _currencyRepository.GetById(update.Id, cancellationToken);
            }
            else
            {
                // If IsDefault status hasn't changed, just update normally
                existingCurrency.IsDefault = update.IsDefault;
                await _currencyRepository.Update(existingCurrency, userId, cancellationToken);
            }

            var updatedCurrencyDto = _mapper.Map<CurrencyDto>(existingCurrency);

            return new GlobalResponse<CurrencyDto>
            {
                StatusCode = 200,
                Message = "واحد پولی با موفقیت به‌روزرسانی شد",
                Data = updatedCurrencyDto
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<CurrencyDto>
            {
                StatusCode = 500,
                Message = $"خطایی در به‌روزرسانی واحد پولی رخ داد: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if currency exists
            var existingCurrency = await _currencyRepository.GetById(id, cancellationToken);

            if (existingCurrency == null)
            {
                return new GlobalResponse<ResponseResultGlobally>
                {
                    StatusCode = 404,
                    Message = $"واحد پولی با شناسه {id} یافت نشد",
                    Data = new ResponseResultGlobally { DoneSuccessfully = false }
                };
            }

            // Check if trying to delete default currency
            if (existingCurrency.IsDefault)
            {
                // Check if there's at least one other currency
                var otherCurrenciesCount = await _currencyRepository.GetAll()
                    .Where(c => c.Id != id)
                    .CountAsync(cancellationToken);

                if (otherCurrenciesCount == 0)
                {
                    return new GlobalResponse<ResponseResultGlobally>
                    {
                        StatusCode = 400,
                        Message = "این تنها واحد پولی موجود در سیستم است و نمی‌توان آن را حذف کرد.",
                        Data = new ResponseResultGlobally { DoneSuccessfully = false }
                    };
                }

                return new GlobalResponse<ResponseResultGlobally>
                {
                    StatusCode = 400,
                    Message = "امکان حذف واحد پولی پیش‌فرض وجود ندارد. لطفاً ابتدا یک واحد پولی دیگر را به عنوان پیش‌فرض تنظیم کنید.",
                    Data = new ResponseResultGlobally { DoneSuccessfully = false }
                };
            }

            await _currencyRepository.Delete(id, userId);

            return new GlobalResponse<ResponseResultGlobally>
            {
                StatusCode = 200,
                Message = "واحد پولی با موفقیت حذف شد",
                Data = new ResponseResultGlobally { DoneSuccessfully = true }
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<ResponseResultGlobally>
            {
                StatusCode = 500,
                Message = $"خطایی در حذف واحد پولی رخ داد: {ex.Message}",
                Data = new ResponseResultGlobally { DoneSuccessfully = false }
            };
        }
    }

    public async Task<GlobalResponse<ResponseResultGlobally>> ChangeDefaultCurrency(long currencyId, bool isDefault)
    {
        try
        {
            // Check if currency exists
            var existingCurrency = await _currencyRepository.GetById(currencyId);

            if (existingCurrency == null)
            {
                return new GlobalResponse<ResponseResultGlobally>
                {
                    StatusCode = 404,
                    Message = $"واحد پولی با شناسه {currencyId} یافت نشد",
                    Data = new ResponseResultGlobally { DoneSuccessfully = false }
                };
            }

            // Additional validation for unsetting default
            if (!isDefault && existingCurrency.IsDefault)
            {
                // Check if there's at least one other currency
                var otherCurrenciesCount = await _currencyRepository.GetAll()
                    .Where(c => c.Id != currencyId)
                    .CountAsync();

                if (otherCurrenciesCount == 0)
                {
                    return new GlobalResponse<ResponseResultGlobally>
                    {
                        StatusCode = 400,
                        Message = "این تنها واحد پولی موجود در سیستم است و نمی‌توان آن را از حالت پیش‌فرض خارج کرد.",
                        Data = new ResponseResultGlobally { DoneSuccessfully = false }
                    };
                }
            }

            var result = await _currencyRepository.ChangeDefaultCurrency(currencyId, isDefault);

            if (result)
            {
                var currency = await _currencyRepository.GetById(currencyId);
                return new GlobalResponse<ResponseResultGlobally>
                {
                    StatusCode = 200,
                    Message = isDefault
                        ? $"واحد پولی {currency?.Name} با موفقیت به عنوان واحد پولی پیش‌فرض تنظیم شد"
                        : $"واحد پولی {currency?.Name} از حالت پیش‌فرض خارج شد",
                    Data = new ResponseResultGlobally { DoneSuccessfully = true }
                };
            }
            else
            {
                return new GlobalResponse<ResponseResultGlobally>
                {
                    StatusCode = 500,
                    Message = "خطایی در تغییر وضعیت واحد پولی پیش‌فرض رخ داد",
                    Data = new ResponseResultGlobally { DoneSuccessfully = false }
                };
            }
        }
        catch (Exception ex)
        {
            return new GlobalResponse<ResponseResultGlobally>
            {
                StatusCode = 500,
                Message = $"خطایی در تغییر وضعیت واحد پولی پیش‌فرض رخ داد: {ex.Message}",
                Data = new ResponseResultGlobally { DoneSuccessfully = false }
            };
        }
    }

    public async Task<GlobalResponse<CurrencyDto>> GetDefaultCurrency()
    {
        try
        {
            var defaultCurrency = await _currencyRepository.GetDefaultCurrency();

            if (defaultCurrency == null)
            {
                return new GlobalResponse<CurrencyDto>
                {
                    StatusCode = 404,
                    Message = "هیچ واحد پولی پیش‌فرضی تنظیم نشده است",
                    Data = null
                };
            }

            var currencyDto = _mapper.Map<CurrencyDto>(defaultCurrency);

            return new GlobalResponse<CurrencyDto>
            {
                StatusCode = 200,
                Message = "واحد پولی پیش‌فرض با موفقیت دریافت شد",
                Data = currencyDto
            };
        }
        catch (InvalidOperationException) // FirstAsync throws when no element found
        {
            return new GlobalResponse<CurrencyDto>
            {
                StatusCode = 404,
                Message = "هیچ واحد پولی پیش‌فرضی تنظیم نشده است",
                Data = null
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<CurrencyDto>
            {
                StatusCode = 500,
                Message = $"خطایی در دریافت واحد پولی پیش‌فرض رخ داد: {ex.Message}",
                Data = null
            };
        }
    }
}
