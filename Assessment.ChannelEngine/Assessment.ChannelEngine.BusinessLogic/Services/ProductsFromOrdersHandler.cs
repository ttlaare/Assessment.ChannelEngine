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
        public async Task<List<ProductDto>> GetTop5ProductsSoldFromOrders(CollectionOfMerchantOrderResponse collectionOfMerchantOrderResponse)
        {
            var products = collectionOfMerchantOrderResponse.GetTop5ProductsSold();
            var productsResponse = await channelEngineHttpClient.GetProducts(products.Select(p => p.MerchantProductNo).ToList());
            return MapProductsResponseToProductDtos(productsResponse, products);
        }

        private List<ProductDto> MapProductsResponseToProductDtos(CollectionOfMerchantProductResponse productsResponse, List<ProductDto> products)
        {
            MerchantProductResponse productResponse;
            foreach (var product in products)
            {
                productResponse = productsResponse.Content.Single(p => p.MerchantProductNo == product.MerchantProductNo);
                product.Name = productResponse.Name;
                product.EAN = productResponse.Ean;
                product.Stock = productResponse.Stock;
            }
            return products;
        }
    }
}
