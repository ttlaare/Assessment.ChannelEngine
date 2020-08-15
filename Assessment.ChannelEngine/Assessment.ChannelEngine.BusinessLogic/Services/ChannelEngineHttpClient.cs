using System;
using System.Net.Http;
using Assessment.ChannelEngine.BusinessLogic.Models.Api;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Assessment.ChannelEngine.BusinessLogic.Interfaces;

namespace Assessment.ChannelEngine.BusinessLogic.Services
{
    public class ChannelEngineHttpClient : IChannelEngineHttpClient
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ChannelEngineHttpClient(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<CollectionOfChannelGlobalChannelResponse> GetNewOrders()
        {
            var httpClient = httpClientFactory.CreateClient("ChannelEngineClient");
            var response = await httpClient.GetAsync("/api/v2/channels?apikey=541b989ef78ccb1bad630ea5b85c6ebff9ca3322");
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            var jsonReader = new StreamReader(stream).ReadToEnd();
            return JsonConvert.DeserializeObject<CollectionOfChannelGlobalChannelResponse>(jsonReader);
        }
    }
}
