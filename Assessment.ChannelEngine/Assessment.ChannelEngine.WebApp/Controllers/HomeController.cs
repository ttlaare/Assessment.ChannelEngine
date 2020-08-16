using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assessment.ChannelEngine.WebApp.Models;
using Assessment.ChannelEngine.BusinessLogic.Interfaces;
using System.Threading.Tasks;

namespace Assessment.ChannelEngine.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChannelEngineHttpClient _channelEngineHttpClient;
        private readonly IProductsFromOrdersHandler _productsFromOrdersHandler;

        public HomeController(ILogger<HomeController> logger, IChannelEngineHttpClient channelEngineHttpClient, IProductsFromOrdersHandler productsFromOrdersHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _channelEngineHttpClient = channelEngineHttpClient ?? throw new ArgumentNullException(nameof(channelEngineHttpClient));
            _productsFromOrdersHandler = productsFromOrdersHandler ?? throw new ArgumentNullException(nameof(productsFromOrdersHandler));
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _channelEngineHttpClient.FetchOrdersWithStatusIN_PROGRESS();
            var products = await _productsFromOrdersHandler.GetTop5ProductsSoldFromOrders(orders);
            return View(products);
        }

        public async Task<IActionResult> SetStock(int index)
        {
            var orders = await _channelEngineHttpClient.FetchOrdersWithStatusIN_PROGRESS();
            var products = await _productsFromOrdersHandler.GetTop5ProductsSoldFromOrders(orders);
            var merchantProductNo = products[index].MerchantProductNo;
            await _channelEngineHttpClient.UpdateProductStock(merchantProductNo, 25);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
