using Assessment.ChannelEngine.BusinessLogic.Models.Api;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assessment.ChannelEngine.BusinessLogic.Interfaces
{
    public interface IChannelEngineHttpClient
    {
        Task<CollectionOfMerchantOrderResponse> FetchOrdersWithStatusIN_PROGRESS();
        Task UpdateProductStock(string merchantProductNo, int stock);
        Task<SingleOfMerchantProductResponse> GetProduct(string merchantProductNo);
        Task<CollectionOfMerchantProductResponse> GetProducts(List<string> merchantProductNoList);
    }
}
