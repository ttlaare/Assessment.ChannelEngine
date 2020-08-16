using Assessment.ChannelEngine.BusinessLogic.Helpers;
using Assessment.ChannelEngine.BusinessLogic.Interfaces;
using Assessment.ChannelEngine.BusinessLogic.Models;
using Assessment.ChannelEngine.BusinessLogic.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.ChannelEngine.BusinessLogic.Services
{
    public class ProductsFromOrdersHandler : IProductsFromOrdersHandler
    {
        private readonly IChannelEngineHttpClient channelEngineHttpClient;

        public ProductsFromOrdersHandler(IChannelEngineHttpClient channelEngineHttpClient)
        {
            this.channelEngineHttpClient = channelEngineHttpClient ?? throw new ArgumentNullException(nameof(channelEngineHttpClient));
        }
        public async Task<List<ProductStatistics>> GetTop5ProductsSoldFromOrders(CollectionOfMerchantOrderResponse collectionOfMerchantOrderResponse)
        {
            var productsStatistics = collectionOfMerchantOrderResponse.GetTop5ProductsSold();
            var productsResponse = await channelEngineHttpClient.GetProducts(productsStatistics.Select(p => p.MerchantProductNo).ToList());
            return MapProductsResponseToProductsStatistics(productsResponse, productsStatistics);
        }

        private List<ProductStatistics> MapProductsResponseToProductsStatistics(CollectionOfMerchantProductResponse productsResponse, List<ProductStatistics> productsStatistics)
        {
            MerchantProductResponse product;
            foreach (var productStatistics in productsStatistics)
            {
                product = productsResponse.Content.Single(p => p.MerchantProductNo == productStatistics.MerchantProductNo);
                productStatistics.Name = product.Name;
                productStatistics.EAN = product.Ean;
            }
            return productsStatistics;
        }
    }
}
