/*
    TradeController has five action methods called "Index()", "BuyOrder()" and "SellOrder()" and "Orders()".
    The controller has to inject the appsettings called "TradingOptions" (from appsettings.json), IFinnhubService, IStocksService, and IConfiguration.
 */

using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.DTO;
using StocksAppProject.Models;
using System.Reflection.Metadata.Ecma335;

namespace StocksAppProject.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        // TradingOptions for loading default attribute data
        private readonly TradingOptions _tradingOptions;

        // FinnhubService for loading data from the DTOs and services
        private readonly IFinnhubService _finnhubService;

        // StocksService for invoking any service methods
        private readonly IStockService _stocksService;

        private readonly IConfiguration _configuration;

        public TradeController(IOptions<TradingOptions> tradingOptions, IFinnhubService finnhubService, IStockService stocksService, IConfiguration configuration)
        {
            _tradingOptions = tradingOptions.Value;
            _finnhubService = finnhubService;
            _stocksService = stocksService;
            _configuration = configuration;
        }

        /*
         * Index method
         * It receives HTTP GET request at route "Trade/Index". 
         * It first gets the "TradingOptions" from appsettings.json using Options pattern.

            It invokes the following methods:
	            1. FinnhubService.GetCompanyProfile() to fetch stock name, stock symbol and other details.
	            2. FinnhubService.GetStockPriceQuote() to fetch current stock price.

            And then it creates an object of "StockTrade" model class and fills essential data such as StockSymbol, StockName, Price and Quantity that are read 
            from the return values of above mentioned service methods i.e. "FinnhubService.GetCompanyProfile()" and "FinnhubService.GetStockPriceQuote()".
            Then it sends the same model object to the view.
         */
        [Route("/")]
        [Route("action")]
        [Route("~/[controller]")]
        public IActionResult Index()
        {
            // Configures a stock symbol to default if it doesn't exist in the options class
            if (string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol)) _tradingOptions.DefaultStockSymbol = "MSFT";

            // Get the company profile from the API server
            Dictionary<string, object>? companyProfileDictionary = _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);

            // Get the stock price quote from the API server
            Dictionary<string, object>? stockQuoteDictionary = _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);

            // Create model object and load data from finnhubService into it
            StockTrade stockTrade = new StockTrade() { StockSymbol = _tradingOptions.DefaultStockSymbol };
            if (companyProfileDictionary != null && stockQuoteDictionary != null)
            {
                stockTrade = new StockTrade()
                {
                    StockSymbol = companyProfileDictionary["ticker"].ToString(),
                    StockName = companyProfileDictionary["name"].ToString(),
                    Quantity = _tradingOptions.DefaultOrderQuantity ?? 0,
                    Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString())
                };
            }

            // Send Finnhub token to view
            ViewBag.FinnhubToken = _configuration["FinnhubToken"];

            return View(stockTrade);
        }

        /*
         * BuyOrder method
         *  • It receives HTTP POST request at route "Trade/BuyOrder".
            • It receives the model object of "BuyOrder" type through model binding.
            • It initializes "DateAndTimeOfOrder" into the model object (i.e. buyOrder).
            • If model state has no errors, it invokes StocksService.CreateBuyOrder() method. Then it redirects to "Trade/Orders" route to display list of orders.
            Alternatively, in case of validation errors in the model object, it reinvokes the same view, along with same model object.
        */
        [Route("[action]")]
        [HttpPost]
        public IActionResult BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            // Update date of order and re-validate model
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(buyOrderRequest);

            if(!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade()
                {
                    StockName = buyOrderRequest.StockName,
                    StockSymbol = buyOrderRequest.StockSymbol,
                    Price = buyOrderRequest.Price,
                    Quantity = buyOrderRequest.Quantity
                };
                return View("Index", stockTrade);
            }

            // Invoke service method
            BuyOrderResponse buyOrderResponse = _stocksService.CreateBuyOrder(buyOrderRequest);

            return RedirectToAction("Orders");
        }

        /*
         * Sell Order method
         *  • It receives HTTP POST request at route "Trade/SellOrder".
            • It receives the model object of "SellOrder" type through model binding.
            • It initializes "DateAndTimeOfOrder" into the model object (i.e. sellOrder).
            • If model state has no errors, it invokes StocksService.CreateSellOrder() method. Then it redirects to "Trade/Orders" route to display list of orders.
            Alternatively, in case of validation errors in the model object, it reinvokes the same view, along with same model object.
        */
        [Route("[action]")]
        [HttpPost]
        public IActionResult SellOrder(SellOrderRequest sellOrderRequest)
        {
            // Update date of order and re-validate model
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(sellOrderRequest);

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade()
                {
                    StockName = sellOrderRequest.StockName,
                    StockSymbol = sellOrderRequest.StockSymbol,
                    Price = sellOrderRequest.Price,
                    Quantity = sellOrderRequest.Quantity
                };
                return View("Index", stockTrade);
            }

            // Invoke service method
            SellOrderResponse sellOrderResponse = _stocksService.CreateSellOrder(sellOrderRequest);

            return RedirectToAction("Orders");
        }

        /*
         * Orders method
         *  • It receives HTTP GET request at route "Trade/Orders".
            • It invokes both the service methods StocksService.GetBuyOrders() and StocksService.GetSellOrders().
            • Then it creates an object of the view model class called 'Orders' and initializes both 'BuyOrders' and 'SellOrders' properties with the data returned by the above called service methods.
            It invokes the "Trade/Orders" view to display list of orders.
        */
        [Route("[action]")]
        public IActionResult Orders()
        {
            // Invoke service methods
            List<BuyOrderResponse>  buyOrders  =  _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrders = _stocksService.GetSellOrders();

            // Create model object
            Orders orders = new Orders()
            {
                BuyOrders = buyOrders,
                SellOrders = sellOrders
            };

            ViewBag.TradingOptions = _tradingOptions;
            return View(orders);
            
        }
    }
}
