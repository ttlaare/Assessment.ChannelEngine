using Assessment.ChannelEngine.BusinessLogic.Interfaces;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.ChannelEngine.ConsoleApp.Services
{
    internal class ChannelEngineHttpClientHandler
    {
        private readonly IChannelEngineHttpClient channelEngineHttpClient;
        private readonly IProductsFromOrdersHandler productsFromOrdersHandler;

        public ChannelEngineHttpClientHandler(IChannelEngineHttpClient channelEngineHttpClient, IProductsFromOrdersHandler productsFromOrdersHandler)
        {
            this.channelEngineHttpClient = channelEngineHttpClient ?? throw new ArgumentNullException(nameof(channelEngineHttpClient));
            this.productsFromOrdersHandler = productsFromOrdersHandler ?? throw new ArgumentNullException(nameof(productsFromOrdersHandler));
        }
        public async Task Run()
        {
            Console.WriteLine("Orders in progress:");
            var inProgressOrders = await channelEngineHttpClient.FetchOrdersWithStatusIN_PROGRESS();
            Console.WriteLine(JsonConvert.SerializeObject(inProgressOrders, Formatting.Indented));
            Console.WriteLine("Press a key to continue");
            Console.ReadKey();

            var products = await productsFromOrdersHandler.GetTop5ProductsSoldFromOrders(inProgressOrders);

            if (products is null || !products.Any())
            {
                Console.WriteLine("No Products to display. Press a key to exit");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Top Products sold:");
            foreach (var product in products)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine($"Merchant Product No: {product.MerchantProductNo}");
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Ean: {product.EAN}");
                Console.WriteLine($"Total Quantity Sold: {product.TotalQuantity}");
                Console.WriteLine($"Stock: {product.Stock}");
            }
            Console.WriteLine("------------------------------");

            Console.WriteLine("Copy paste the 'Merchant Product No' whose stock you want to update to 25");
            var selectedProductNo = Console.ReadLine().Trim();
            await channelEngineHttpClient.UpdateProductStock(selectedProductNo, 25);

            var updatedProduct = await channelEngineHttpClient.GetProduct(selectedProductNo);

            Console.WriteLine("Updating product stock was successful.");
            Console.WriteLine("Updated product:");
            Console.WriteLine(JsonConvert.SerializeObject(updatedProduct, Formatting.Indented));
            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }
    }
}
