using AvaCrm.Domain.Enums.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaCrm.Persistence.Repositories.Commons
{
    public class UnitRepository : GenericRepository<Unit>, IUnitRepository
    {
        private readonly AvaCrmContext _context;
        public UnitRepository(AvaCrmContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Unit>> GetUnitsByUnitCategory(UnitCategory unitCategory)
        {
           return await _context.Units.Where(u=> !u.IsDelete && u.Category  == unitCategory).ToListAsync();
        }
    }
}
