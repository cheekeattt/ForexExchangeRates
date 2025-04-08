using ForexExchangeRates.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ForexExchangeRates.Controllers
{
    public class ExchangeRateController : Controller
    {
        private readonly ILogger<ExchangeRateController> _logger;
        private readonly IExchangeRateService _exchangeRateService;
        public ExchangeRateController(ILogger<ExchangeRateController> logger, IExchangeRateService exchangeRateService)
        {
            _logger = logger;
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetExchangeRateAsync(string baseCurrency, string targetCurrency)
        {
            var rate = await _exchangeRateService.GetExchangeRateAsync(baseCurrency, targetCurrency);
            return Json(rate);
        }

        [HttpGet]
        public async Task<IActionResult> GetSupportedCurrenciesAsync()
        {
            var rate = await _exchangeRateService.GetSupportedCurrencies();
            return Json(rate);
        }
    }
}
