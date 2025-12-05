using AvaCrm.Application.DTOs.Commons.Currencies;
using AvaCrm.Application.Features.Commons;
using AvaCrm.Application.Pagination;
using AvaCrm.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class CurrenciesController : BaseController
{
    private readonly ICurrencyService _currencyService;

    public CurrenciesController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    /// <summary>
    /// دریافت لیست ارزها با صفحه‌بندی
    /// </summary>
    /// <param name="request">اطلاعات صفحه‌بندی</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(GlobalResponse<PaginatedResult<CurrencyDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllCurrencies([FromQuery] PaginationRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _currencyService.GetAllCurrencies(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// دریافت ارز بر اساس شناسه
    /// </summary>
    /// <param name="id">شناسه ارز</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(GlobalResponse<CurrencyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCurrencyById(long id, CancellationToken cancellationToken = default)
    {
        var result = await _currencyService.GetCurrencyById(id, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// دریافت ارز پیش‌فرض
    /// </summary>
    /// <returns></returns>
    [HttpGet("default")]
    [ProducesResponseType(typeof(GlobalResponse<CurrencyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDefaultCurrency()
    {
        var result = await _currencyService.GetDefaultCurrency();
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// ایجاد ارز جدید
    /// </summary>
    /// <param name="createDto">اطلاعات ارز جدید</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(GlobalResponse<CurrencyDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCurrency([FromBody] CreateCurrencyDto createDto, CancellationToken cancellationToken = default)
    {
         var result = await _currencyService.CreateCurrency(createDto, GetCurrentUserId(), cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// به‌روزرسانی ارز
    /// </summary>
    /// <param name="updateDto">اطلاعات به‌روزرسانی ارز</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(typeof(GlobalResponse<CurrencyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCurrency([FromBody] UpdateCurrencyDto updateDto, CancellationToken cancellationToken = default)
    {
         var result = await _currencyService.UpdateCurrency(updateDto, GetCurrentUserId(), cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// تغییر وضعیت پیش‌فرض بودن ارز
    /// </summary>
    /// <param name="currencyId">شناسه ارز</param>
    /// <param name="isDefault">آیا پیش‌فرض شود؟</param>
    /// <returns></returns>
    [HttpPatch("{currencyId:long}/set-default")]
    [ProducesResponseType(typeof(GlobalResponse<ResponseResultGlobally>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangeDefaultCurrency(long currencyId, [FromQuery] bool isDefault = true)
    {
        var result = await _currencyService.ChangeDefaultCurrency(currencyId, isDefault);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// حذف ارز
    /// </summary>
    /// <param name="id">شناسه ارز</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(GlobalResponse<ResponseResultGlobally>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCurrency(long id, CancellationToken cancellationToken = default)
    {
        var result = await _currencyService.Delete(id, GetCurrentUserId(), cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}
