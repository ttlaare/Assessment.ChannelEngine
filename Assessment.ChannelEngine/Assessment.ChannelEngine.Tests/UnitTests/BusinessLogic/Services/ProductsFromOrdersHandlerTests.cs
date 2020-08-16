using Assessment.ChannelEngine.BusinessLogic.Interfaces;
using Assessment.ChannelEngine.BusinessLogic.Models.Api;
using Assessment.ChannelEngine.BusinessLogic.Services;
using Assessment.ChannelEngine.Tests.Builders;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assessment.ChannelEngine.Tests.UnitTests.BusinessLogic.Services
{
    internal class ProductsFromOrdersHandlerTests
    {
        [Test]
        public async Task GetTop5ProductsSoldFromOrders_Should_ReturnListOfProductStatistics()
        {
            var expected = new ProductsStatisticsBuilder()
                            .WithProductStatisticsIs("ProductNo1", 200,"Name1","EAN1")
                            .WithProductStatisticsIs("ProductNo2", 150, "Name2", "EAN2")
                            .WithProductStatisticsIs("ProductNo3", 50, "Name3", "EAN3")
                            .Build();

            var lines1 = new MerchantOrderLineResponsesBuilder()
                        .WithMerchantOrderLineResponseIs("ProductNo3", 50)
                        .WithMerchantOrderLineResponseIs("ProductNo2", 100)
                        .WithMerchantOrderLineResponseIs("ProductNo1", 50)
                        .Build();

            var lines2 = new MerchantOrderLineResponsesBuilder()
                        .WithMerchantOrderLineResponseIs("ProductNo1", 50)
                        .WithMerchantOrderLineResponseIs("ProductNo2", 50)
                        .WithMerchantOrderLineResponseIs("ProductNo1", 100)
                        .Build();

            var merchantOrderResponses = new MerchantOrderResponsesBuilder()
                                        .WithmerchantOrderResponseIs(lines1)
                                        .WithmerchantOrderResponseIs(lines2)
                                        .Build();

            var collectionMerchantOrderResponse = new CollectionOfMerchantOrderResponse() { Content = merchantOrderResponses };

            var productRespones = new MerchantProductResponsesBuilder()
                                .WithMerchantProductResponseIs("ProductNo3", "Name3", "EAN3")
                                .WithMerchantProductResponseIs("ProductNo2", "Name2", "EAN2")
                                .WithMerchantProductResponseIs("ProductNo1", "Name1", "EAN1")
                                .Build();

            var collectionMerchantProductResponse = new CollectionOfMerchantProductResponse() { Content = productRespones };

            var mockedchannelEngineHttpClient = new Mock<IChannelEngineHttpClient>();
            mockedchannelEngineHttpClient.Setup(c => c.GetProducts(It.IsAny<List<string>>())).ReturnsAsync(collectionMerchantProductResponse);

            var sut = new ProductsFromOrdersHandler(mockedchannelEngineHttpClient.Object);

            //Act
            var result = await sut.GetTop5ProductsSoldFromOrders(collectionMerchantOrderResponse);

            //Assert
            result.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
        }
    }
}
