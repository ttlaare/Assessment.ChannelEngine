using Assessment.ChannelEngine.BusinessLogic.Models;
using Assessment.ChannelEngine.BusinessLogic.Models.Api;
using System.Collections.Generic;
using System.Linq;

namespace Assessment.ChannelEngine.BusinessLogic.Helpers
{
    public static class CollectionOfMerchantOrderResponseExtensions
    {
        public static List<ProductStatistics> GetTop5ProductsSold(this CollectionOfMerchantOrderResponse collectionOfMerchantOrderResponse)
        {
            var lines = collectionOfMerchantOrderResponse.Content.SelectMany(c => c.Lines);
            return lines
                .GroupBy(
                    l => l.MerchantProductNo,
                    (key, l) => new ProductStatistics{
                        MerchantProductNo = key,
                        TotalQuantity = l.Select(l => l.Quantity).Aggregate((a,b) => a + b)
                        })
                .OrderByDescending(p => p.TotalQuantity)
                .Take(5)
                .ToList();
        }
    }
}
