using AvaCrm.Application.DTOs.Commons.Units;
using AvaCrm.Application.Pagination;
using AvaCrm.Domain.Enums.Commons;

namespace AvaCrm.Application.Features.Commons;

public interface IUnitService
{
    Task<GlobalResponse<PaginatedResult<UnitDto>>> GetAllUnits(PaginationRequest request, CancellationToken cancellationToken = default);
    Task<GlobalResponse<List<UnitDto>>> GetUnitsByCategory(UnitCategory category);
    Task<GlobalResponse<UnitDto>> GetUnitById(long unitId, CancellationToken cancellationToken = default);
    Task<GlobalResponse<UnitDto>> CreateUnit(CreateUnitDto create, long userId, CancellationToken cancellationToken = default);
    Task<GlobalResponse<UnitDto>> UpdateUnit(UpdateUnitDto update, long userId, CancellationToken cancellationToken = default);
    Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default);
}
