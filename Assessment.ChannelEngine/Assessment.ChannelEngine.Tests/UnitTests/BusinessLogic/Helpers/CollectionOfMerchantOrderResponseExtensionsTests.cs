﻿using Assessment.ChannelEngine.BusinessLogic.Helpers;
using Assessment.ChannelEngine.BusinessLogic.Models.Api;
using Assessment.ChannelEngine.Tests.Builders;
using FluentAssertions;
using NUnit.Framework;

namespace Assessment.ChannelEngine.Tests.UnitTests.BusinessLogic.Helpers
{
    internal class CollectionOfMerchantOrderResponseExtensionsTests
    {
        [Test]
        public void GetGetTop5ProductsSold_Should_ReturnTop5ProductsSold()
        {
            //Arrange
            var expected = new ProductsStatisticsBuilder()
                .WithProductStatisticsIs("ProductNo1", 200)
                .WithProductStatisticsIs("ProductNo2", 150)
                .WithProductStatisticsIs("ProductNo3", 50)

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

            var sut = new CollectionOfMerchantOrderResponse() { Content = merchantOrderResponses };

            //Act
            var result = sut.GetTop5ProductsSold();

            //Assert
            result.Should().BeEquivalentTo(expected, option => option.WithStrictOrdering());
        }
    }
}