using AvaCrm.Domain.Enums.Commons;

namespace AvaCrm.Domain.Contracts.Commons;

public interface IUnitRepository :IGenericRepository<Unit>
{
    Task<List<Unit>> GetUnitsByUnitCategory(UnitCategory unitCategory);
}
