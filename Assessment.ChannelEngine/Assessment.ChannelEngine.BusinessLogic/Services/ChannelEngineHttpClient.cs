using System;
using System.Net.Http;
using Assessment.ChannelEngine.BusinessLogic.Models.Api;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Assessment.ChannelEngine.BusinessLogic.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using System.Net.Http.Headers;
using System.Text;

namespace Assessment.ChannelEngine.BusinessLogic.Services
{
    public class ChannelEngineHttpClient : IChannelEngineHttpClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly static string ApiKey = "apikey=541b989ef78ccb1bad630ea5b85c6ebff9ca3322";

        public ChannelEngineHttpClient(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<CollectionOfMerchantOrderResponse> FetchOrdersWithStatusIN_PROGRESS()
        {
            var httpClient = httpClientFactory.CreateClient("ChannelEngineClient");
            var response = await httpClient.GetAsync($"/api/v2/orders?statuses=IN_PROGRESS&{ApiKey}");

            response.EnsureSuccessStatusCode();

            return await GetContentFromResponse<CollectionOfMerchantOrderResponse>(response);
        }

        public async Task UpdateProductStock(string merchantProductNo, int stock)
        {
            var httpClient = httpClientFactory.CreateClient("ChannelEngineClient");

            var patchDoc = new JsonPatchDocument<MerchantProductRequest>();
            patchDoc.Replace(p => p.Stock, stock);

            var serializedChangeSet = JsonConvert.SerializeObject(patchDoc);

            var request = new HttpRequestMessage(HttpMethod.Patch,
                $"api/v2/products/{merchantProductNo}?{ApiKey}");


            request.Content = new StringContent(serializedChangeSet);
            request.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/json-patch+json");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<SingleOfMerchantProductResponse> GetProduct(string merchantProductNo)
        {
            var httpClient = httpClientFactory.CreateClient("ChannelEngineClient");
            var response = await httpClient.GetAsync($"/api/v2/products/{merchantProductNo}?{ApiKey}");
            response.EnsureSuccessStatusCode();

            return await GetContentFromResponse<SingleOfMerchantProductResponse>(response);
        }

        public async Task<CollectionOfMerchantProductResponse> GetProducts(List<string> merchantProductNoList)
        {
            var httpClient = httpClientFactory.CreateClient("ChannelEngineClient");

            var urlStringBuilder = new StringBuilder();

            urlStringBuilder.Append("/api/v2/products?");
            foreach (var productNo in merchantProductNoList)
            {
                urlStringBuilder.Append($"merchantProductNoList{productNo}&");
            }
            urlStringBuilder.Append($"{ApiKey}");

            var response = await httpClient.GetAsync(urlStringBuilder.ToString());
            response.EnsureSuccessStatusCode();
            return await GetContentFromResponse<CollectionOfMerchantProductResponse>(response);
        }

        private async Task<T> GetContentFromResponse<T>(HttpResponseMessage response)
        {
            using var stream = await response.Content.ReadAsStreamAsync();
            var jsonReader = new StreamReader(stream).ReadToEnd();
            return JsonConvert.DeserializeObject<T>(jsonReader);
        }
    }
}
