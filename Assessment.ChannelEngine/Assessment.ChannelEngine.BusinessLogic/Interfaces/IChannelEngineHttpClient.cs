using Assessment.ChannelEngine.BusinessLogic.Models.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.ChannelEngine.BusinessLogic.Interfaces
{
    public interface IChannelEngineHttpClient
    {
        Task<CollectionOfChannelGlobalChannelResponse> GetNewOrders();
    }
}
