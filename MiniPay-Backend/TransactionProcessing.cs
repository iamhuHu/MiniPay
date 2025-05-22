using MiniPay_Contracts;

namespace MiniPay_Backend
{
    public class TransactionProcessing(Transaction transaction)
    {
        private static int TransactionCount = 0;        

        public PaymentResponse ExecuteTransaction()
        {           
            var provider = ProviderStorage.GetStoredProviders()?.SingleOrDefault(d => d.Id == transaction.ProviderId);
            if (provider != null)
            {
                return ProcessViaProvider(provider);
            }

            return new PaymentResponse
            {
                Status = "no valid provider"
            };
        }

        private PaymentResponse ProcessViaProvider(Provider provider)
        {
            //switch (provider)
            //{
            //    provider.EndpointUrl
            //}

            // dummy transaction success
            bool isSuccess = true;
            if (++TransactionCount % 3 == 0)
            {
                // every third fails
                isSuccess = false;
            }

            var paymentResponse = new PaymentResponse
            {
                ReferenceId = transaction.ReferenceId,
                Timestamp = DateTime.Now,
                TransactionId = Guid.NewGuid().ToString(),
                Message = $"{provider.Name}",
                Status = isSuccess ? "success" : "failed"
            };

            return paymentResponse;            
        }
    }
}
