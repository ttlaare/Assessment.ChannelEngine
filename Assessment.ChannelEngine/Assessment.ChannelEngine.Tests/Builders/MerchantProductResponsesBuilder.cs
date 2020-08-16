using Assessment.ChannelEngine.BusinessLogic.Models.Api;
using System.Collections.Generic;

namespace Assessment.ChannelEngine.Tests.Builders
{
    internal class MerchantProductResponsesBuilder
    {
        private List<MerchantProductResponse> merchantProductResponses = new List<MerchantProductResponse>();

        public MerchantProductResponsesBuilder WithMerchantProductResponseIs(string merchantProductNo, string name, string ean)
        {
            merchantProductResponses.Add(new MerchantProductResponse
            {
                MerchantProductNo = merchantProductNo,
                Name = name,
                Ean = ean
            });
            return this;
        }

        public List<MerchantProductResponse> Build()
        {
            return merchantProductResponses;
        }
    }
}
