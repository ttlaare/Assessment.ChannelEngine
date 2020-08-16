using Assessment.ChannelEngine.BusinessLogic.Models;
using Assessment.ChannelEngine.BusinessLogic.Models.Api;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assessment.ChannelEngine.BusinessLogic.Interfaces
{
    public interface IProductsFromOrdersHandler
    {
        Task<List<ProductDto>> GetTop5ProductsSoldFromOrders(CollectionOfMerchantOrderResponse collectionOfMerchantOrderResponse);
    }
}
