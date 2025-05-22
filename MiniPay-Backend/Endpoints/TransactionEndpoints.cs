using MiniPay_Contracts;

namespace MiniPay_Backend.Endpoints
{    
    public static class TransactionEndpoints
    {
        private const string EndpointTag = "Transaction";        

        public static void UseTransactionEndpoints(this WebApplication app)
        {
            app.MapGet("/getTransactions", (int? providerId) 
                => GetTransactions(providerId)).WithTags(EndpointTag);
           
            app.MapPost("/doTransaction", (PaymentRequest request) 
                => DoTransaction(request)).WithTags(EndpointTag);

            app.MapPost("/deleteTransaction", (string id) 
                => DeleteTransaction(id)).WithTags(EndpointTag);
        }
        
        private static List<Transaction>? GetTransactions(int? providerId)
        {
            var transactions = TransactionStorage.GetStoredTransactions();
            if (providerId > -1 && transactions != null)
            {
                return [.. transactions.Where(d => d.ProviderId == providerId)];
            }
            return transactions;
        }

        private static IResult DeleteTransaction(string id)
        {
            var transactions = TransactionStorage.GetStoredTransactions();
            var transactionToDelete = transactions?.FirstOrDefault(d => d.Id == id);
            if (transactions != null && transactionToDelete != null)
            {
                transactions.Remove(transactionToDelete);

                TransactionStorage.StoreTransactions(transactions);
            }
            
            return Results.Ok(true);
        }

        private static IResult DoTransaction(PaymentRequest request)
        {
            var transactions = TransactionStorage.GetStoredTransactions();
            if (transactions != null)
            {
                var transaction = new Transaction
                {
                    Amount = request.Amount,
                    Currency = request.Currency,
                    Description = request.Description,
                    ProviderId = request.ProviderId,
                    ReferenceId = Guid.NewGuid().ToString()
                };

                var process = new TransactionProcessing(transaction);
                var paymentResponse = process.ExecuteTransaction();

                transaction.Status = paymentResponse.Status;
                transaction.Id = paymentResponse.TransactionId;
                transaction.Message = paymentResponse.Message;
                transaction.Timestamp = paymentResponse.Timestamp;
                transactions.Add(transaction);

                TransactionStorage.StoreTransactions(transactions);

                return Results.Ok(true);                
            }
            return Results.Ok(false);
        }
    }
}
