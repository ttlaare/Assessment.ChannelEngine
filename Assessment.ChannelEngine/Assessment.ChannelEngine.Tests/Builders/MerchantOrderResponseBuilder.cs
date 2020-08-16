using Assessment.ChannelEngine.BusinessLogic.Models.Api;
using System.Collections.Generic;

namespace Assessment.ChannelEngine.Tests.Builders
{
    internal class MerchantOrderResponsesBuilder
    {
        private List<MerchantOrderResponse> merchantOrderResponses = new List<MerchantOrderResponse>();

        public MerchantOrderResponsesBuilder WithmerchantOrderResponseIs(List<MerchantOrderLineResponse> lines)
        {
            merchantOrderResponses.Add(new MerchantOrderResponse
            {
                Lines = lines
            });
            return this;
        }

        public List<MerchantOrderResponse> Build()
        {
            return merchantOrderResponses;
        }
    }
}
