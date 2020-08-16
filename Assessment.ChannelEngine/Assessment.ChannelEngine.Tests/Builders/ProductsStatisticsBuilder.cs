using Assessment.ChannelEngine.BusinessLogic.Models;
using System.Collections.Generic;

namespace Assessment.ChannelEngine.Tests.Builders
{
    internal class ProductsStatisticsBuilder
    {
        private List<ProductStatistics> productStatistics = new List<ProductStatistics>();

        public ProductsStatisticsBuilder WithProductStatisticsIs(string productNo, int quantity, string name, string ean)
        {
            productStatistics.Add(new ProductStatistics
            {
                MerchantProductNo = productNo,
                Name = name,
                EAN = ean,
                TotalQuantity = quantity
            });
            return this;
        }

        public ProductsStatisticsBuilder WithProductStatisticsIs(string productNo, int quantity)
        {
            productStatistics.Add(new ProductStatistics
            {
                MerchantProductNo = productNo,
                TotalQuantity = quantity
            });
            return this;
        }

        public List<ProductStatistics> Build()
        {
            return productStatistics;
        }
    }
}
