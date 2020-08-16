using Assessment.ChannelEngine.BusinessLogic.Interfaces;
using Assessment.ChannelEngine.BusinessLogic.Services;
using Assessment.ChannelEngine.ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Assessment.ChannelEngine.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);
                var serviceProvider = serviceCollection.BuildServiceProvider();
                await serviceProvider.GetService<ChannelEngineHttpClientHandler>().Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex}");
                Console.WriteLine("Press a key to exit");
                Console.ReadKey();
                throw;
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient("ChannelEngineClient", client =>
            {
                client.BaseAddress = new Uri("https://api-dev.channelengine.net");
                client.Timeout = new TimeSpan(0, 0, 30);
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            serviceCollection.AddScoped<IChannelEngineHttpClient, ChannelEngineHttpClient>();
            serviceCollection.AddScoped<IProductsFromOrdersHandler, ProductsFromOrdersHandler>();
            serviceCollection.AddScoped<ChannelEngineHttpClientHandler>();
        }
    }
}
