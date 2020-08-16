using Assessment.ChannelEngine.BusinessLogic.Models;
using System.Collections.Generic;

namespace Assessment.ChannelEngine.Tests.Builders
{
    internal class ProductDtosBuilder
    {
        private List<ProductDto> products = new List<ProductDto>();

        public ProductDtosBuilder WithProductDtoIs(string productNo, int quantity, string name, string ean, int stock)
        {
            products.Add(new ProductDto
            {
                MerchantProductNo = productNo,
                Name = name,
                EAN = ean,
                TotalQuantity = quantity,
                Stock = stock
            });
            return this;
        }

        public ProductDtosBuilder WithProductDtoIs(string productNo, int quantity)
        {
            products.Add(new ProductDto
            {
                MerchantProductNo = productNo,
                TotalQuantity = quantity
            });
            return this;
        }

        public List<ProductDto> Build()
        {
            return products;
        }
    }
}
