using ForexExchangeRates.Constants;
using ForexExchangeRates.Interfaces;
using ForexExchangeRates.Models;
using Newtonsoft.Json.Linq;

namespace ForexExchangeRates.Services.ExchangeRate
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiUrl;
        public ExchangeRateService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ExchangeRate:ExchangeRateApiKey"]!;
            _apiUrl = configuration["ExchangeRate:ExchangeRateApiURL"]!;
        }

        public async Task<List<CurrencyCode>?> GetSupportedCurrencies()
        {
            //e.g GET https://v6.exchangerate-api.com/v6/YOUR-API-KEY/codes
            List<CurrencyCode>? currencies = new();

            try
            {
                var url = $"{_apiUrl}/{_apiKey}{ExchangeRateApiUrls.GetSupportedCurrencies}";
                var response = await _httpClient.GetStringAsync(url);
                var json = JObject.Parse(response);
                if (json["result"]!.ToString() == "success" && json["supported_codes"] is JArray supportedCodes)
                {
                    foreach (var item in supportedCodes)
                    {
                        currencies.Add(new CurrencyCode
                        {
                            Code = item[0]?.ToString() ?? string.Empty,
                            Name = item[1]?.ToString() ?? string.Empty,
                        });
                    }
                }
                else
                {
                    currencies = null;
                }
            }
            catch ( Exception )
            {
                currencies = null;
            }

            return currencies ;
        }

        public async Task<decimal?> GetExchangeRateAsync(string baseCurr, string targetCurr)
        {
            // e.g GET https://v6.exchangerate-api.com/v6/YOUR-API-KEY/pair/EUR/GBP/AMOUNT

            try
            {
                var url = $"{_apiUrl}/{_apiKey}/pair/{baseCurr}/{targetCurr}";
                var response = await _httpClient.GetStringAsync(url);
                var json = JObject.Parse(response);

                if (json["result"]!.ToString() == "success")
                {
                    var rate = json["conversion_rate"]?.ToString();

                    if (decimal.TryParse(rate, out var conversionRate))
                    {
                        return conversionRate;
                    }
                }
            }
            catch (Exception)
            {
                
            }

            return null;
        }

    }
}
