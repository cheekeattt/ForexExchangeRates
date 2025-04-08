using ForexExchangeRates.Models;

namespace ForexExchangeRates.Interfaces
{
    public interface IExchangeRateService
    {
        public Task<List<CurrencyCode>?> GetSupportedCurrencies();
        public Task<decimal?> GetExchangeRateAsync(string baseCurr, string targetCurr);
    }
}
