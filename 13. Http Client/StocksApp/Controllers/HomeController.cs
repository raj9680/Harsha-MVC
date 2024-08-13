using Dependency_Injection.ForOptionsPattern;
using Dependency_Injection.Modals;
using Dependency_Injection.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Dependency_Injection.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinHubService _finHubService;
        private readonly IOptions<TradingOptions> _options;
        public HomeController(FinHubService finHubService, IOptions<TradingOptions> options)
        {
            _finHubService = finHubService;
            _options = options;
        }


        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (_options.Value.DefaultStockSymbol == null)
                _options.Value.DefaultStockSymbol = "MSFT";

            Dictionary<string, object>? response = await _finHubService.GetStockPriceQuote(_options.Value.DefaultStockSymbol);

            // Passing response data to Model Class
            StocksEntity stocksEntity = new StocksEntity()
            {
                StockSymbol = _options.Value.DefaultStockSymbol,

                // response is of object type, hence converting the same to required type
                CurrentPrice = Convert.ToDouble(response["c"].ToString()),
                HighestPrice = Convert.ToDouble(response["h"].ToString()),
                LowestPrice = Convert.ToDouble(response["l"].ToString()),
                OpenPrice = Convert.ToDouble(response["o"].ToString())
            };

            return View(stocksEntity);
        }
    }
}
