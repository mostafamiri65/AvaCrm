using AvaCrm.Application.DTOs.Commons.Currencies;
using AvaCrm.Application.Pagination;

namespace AvaCrm.Application.Features.Commons;

public interface ICurrencyService
{
    Task<GlobalResponse<PaginatedResult<CurrencyDto>>> GetAllCurrencies(PaginationRequest request, CancellationToken cancellationToken = default);
    Task<GlobalResponse<CurrencyDto>> GetCurrencyById(long currencyId, CancellationToken cancellationToken = default);
    Task<GlobalResponse<CurrencyDto>> CreateCurrency(CreateCurrencyDto create, long userId, CancellationToken cancellationToken = default);
    Task<GlobalResponse<CurrencyDto>> UpdateCurrency(UpdateCurrencyDto update, long userId, CancellationToken cancellationToken = default);
    Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default);
    Task<GlobalResponse<ResponseResultGlobally>> ChangeDefaultCurrency(long currencyId, bool isDefault);
    Task<GlobalResponse<CurrencyDto>> GetDefaultCurrency();
}
