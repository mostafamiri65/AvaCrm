

namespace AvaCrm.Persistence.Repositories.Commons;

public class CurrencyRepository : GenericRepository<Currency>, ICurrencyRepository
{
    private readonly AvaCrmContext _context;
    public CurrencyRepository(AvaCrmContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<Currency> Create(Currency entity, long userId, bool saveNow = true)
    {
        if (entity.IsDefault)
        {
            var defaultCurrency = await _context.Currencies.FirstOrDefaultAsync(c => c.IsDefault && !c.IsDelete);
            if (defaultCurrency != null) defaultCurrency.IsDefault = false;
            await _context.SaveChangesAsync();
        }
        return await base.Create(entity, userId, saveNow);
    }
    public async Task<bool> ChangeDefaultCurrency(long currencyId, bool isDefault)
    {
        // 1) Load the target currency first
        var currency = await _context.Currencies
            .FirstOrDefaultAsync(c => c.Id == currencyId && !c.IsDelete);

        if (currency == null)
            return false;

        // ------------------------------------------
        // CASE A: User is trying to set this currency as DEFAULT
        // ------------------------------------------
        if (isDefault)
        {
            // Find currently default currency
            var currentDefault = await _context.Currencies
                .FirstOrDefaultAsync(c => c.IsDefault && !c.IsDelete);

            // If there is another default currency, remove its default flag
            if (currentDefault != null && currentDefault.Id != currencyId)
            {
                currentDefault.IsDefault = false;
            }

            // Set requested currency as default
            currency.IsDefault = true;

            await _context.SaveChangesAsync();
            return true;
        }

        // ------------------------------------------
        // CASE B: User is trying to UNSET this currency (isDefault = false)
        // ------------------------------------------
        if (!isDefault)
        {
            // If this currency IS default, we must select another one as default
            if (currency.IsDefault)
            {
                // Find the last modified active currency except this one
                var fallbackCurrency = await _context.Currencies
                    .Where(c => !c.IsDelete && c.Id != currencyId)
                    .OrderByDescending(c => c.Id)
                    .FirstOrDefaultAsync();

                // If there is another currency, set it as default
                if (fallbackCurrency != null)
                {
                    fallbackCurrency.IsDefault = true;
                }
            }

            // Unset default on this currency
            currency.IsDefault = false;

            await _context.SaveChangesAsync();
            return true;
        }

        return true;

    }

    public async Task<Currency> GetDefaultCurrency()
    {
       return await  _context.Currencies.FirstAsync(c => c.IsDefault && !c.IsDelete);
    }

}
