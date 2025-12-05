namespace AvaCrm.Domain.Contracts.Commons;

public interface ICurrencyRepository : IGenericRepository<Currency>
{
    Task<bool> ChangeDefaultCurrency(long currencyId,bool isDefault);
    Task<Currency> GetDefaultCurrency();
}
