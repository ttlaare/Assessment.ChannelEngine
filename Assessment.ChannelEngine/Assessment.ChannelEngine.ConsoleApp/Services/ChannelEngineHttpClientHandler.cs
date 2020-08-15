using Assessment.ChannelEngine.BusinessLogic.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assessment.ChannelEngine.ConsoleApp.Services
{
    internal class ChannelEngineHttpClientHandler
    {
        private readonly IChannelEngineHttpClient channelEngineHttpClient;

        public ChannelEngineHttpClientHandler(IChannelEngineHttpClient channelEngineHttpClient)
        {
            this.channelEngineHttpClient = channelEngineHttpClient ?? throw new ArgumentNullException(nameof(channelEngineHttpClient));
        }
        public async Task Run()
        {
            var newOrders = await channelEngineHttpClient.GetNewOrders();
            Console.WriteLine(JsonConvert.SerializeObject(newOrders,Formatting.Indented));
            Console.ReadKey();
        }
    }
}
