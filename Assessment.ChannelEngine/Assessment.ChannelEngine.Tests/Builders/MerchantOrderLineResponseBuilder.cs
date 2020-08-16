using Assessment.ChannelEngine.BusinessLogic.Models.Api;
using System.Collections.Generic;

namespace Assessment.ChannelEngine.Tests.Builders
{
    internal class MerchantOrderLineResponsesBuilder
    {
        private List<MerchantOrderLineResponse> MerchantOrderLineResponses = new List<MerchantOrderLineResponse>();

        public MerchantOrderLineResponsesBuilder WithMerchantOrderLineResponseIs(string merchantProductNo, int quantity)
        {
            MerchantOrderLineResponses.Add(new MerchantOrderLineResponse
            {
                MerchantProductNo = merchantProductNo,
                Quantity = quantity
            });
            return this;
        }

        public List<MerchantOrderLineResponse> Build()
        {
            return MerchantOrderLineResponses;
        }
    }
}
