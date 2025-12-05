using AvaCrm.Application.DTOs.Commons.Units;
using AvaCrm.Application.Features.Commons;
using AvaCrm.Application.Pagination;
using AvaCrm.Application.Responses;
using AvaCrm.Domain.Enums.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class UnitsController : BaseController
{
    private readonly IUnitService _unitService;

    public UnitsController(IUnitService unitService)
    {
        _unitService = unitService;
    }

    /// <summary>
    /// دریافت لیست واحدها با صفحه‌بندی
    /// </summary>
    /// <param name="request">اطلاعات صفحه‌بندی</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(GlobalResponse<PaginatedResult<UnitDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllUnits([FromQuery] PaginationRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _unitService.GetAllUnits(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// دریافت واحد بر اساس شناسه
    /// </summary>
    /// <param name="id">شناسه واحد</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(GlobalResponse<UnitDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUnitById(long id, CancellationToken cancellationToken = default)
    {
        var result = await _unitService.GetUnitById(id, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// دریافت واحدها بر اساس دسته‌بندی
    /// </summary>
    /// <param name="category">دسته‌بندی واحد</param>
    /// <returns></returns>
    [HttpGet("by-category/{category}")]
    [ProducesResponseType(typeof(GlobalResponse<List<UnitDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUnitsByCategory(UnitCategory category)
    {
        var result = await _unitService.GetUnitsByCategory(category);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// ایجاد واحد جدید
    /// </summary>
    /// <param name="createDto">اطلاعات واحد جدید</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(GlobalResponse<UnitDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateUnit([FromBody] CreateUnitDto createDto, CancellationToken cancellationToken = default)
    {
        var result = await _unitService.CreateUnit(createDto, GetCurrentUserId(), cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// به‌روزرسانی واحد
    /// </summary>
    /// <param name="updateDto">اطلاعات به‌روزرسانی واحد</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(typeof(GlobalResponse<UnitDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUnit([FromBody] UpdateUnitDto updateDto, CancellationToken cancellationToken = default)
    {
        var result = await _unitService.UpdateUnit(updateDto, GetCurrentUserId(), cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// حذف واحد
    /// </summary>
    /// <param name="id">شناسه واحد</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(GlobalResponse<ResponseResultGlobally>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUnit(long id, CancellationToken cancellationToken = default)
    {
        var result = await _unitService.Delete(id, GetCurrentUserId(), cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}
