using Assessment.ChannelEngine.BusinessLogic.Helpers;
using Assessment.ChannelEngine.BusinessLogic.Models.Api;
using Assessment.ChannelEngine.Tests.Builders;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Assessment.ChannelEngine.Tests.UnitTests.BusinessLogic.Helpers
{
    internal class CollectionOfMerchantOrderResponseExtensionsTests
    {
        [Test]
        public void GetGetTop5ProductsSold_Should_ReturnTop5ProductsSold()
        {
            //Arrange
            var expected = new ProductDtosBuilder()
                .WithProductDtoIs("ProductNo1", 200)
                .WithProductDtoIs("ProductNo2", 150)
                .WithProductDtoIs("ProductNo3", 50)

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

        [Test]
        public void GetGetTop5ProductsSold_Given_Null_Should_ThrowArgumentNullException()
        {
            //Arrange
            CollectionOfMerchantOrderResponse sut = null;

            //Act & Assert
            sut.Invoking(s => s.GetTop5ProductsSold()).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void GetGetTop5ProductsSold_Given_ContentIsNull_Should_ThrowArgumentNullException()
        {
            //Arrange
            var sut = new CollectionOfMerchantOrderResponse() { Content = null };

            //Act & Assert
            sut.Invoking(s => s.GetTop5ProductsSold()).Should().Throw<ArgumentNullException>();
        }
    }
}
