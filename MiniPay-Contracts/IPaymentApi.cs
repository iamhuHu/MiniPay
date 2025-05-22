namespace MiniPay_Contracts
{
    public interface IPaymentApi
    {
        Task<List<Provider>?> GetProviders();
        Task<Provider?> GetProvider(int providerId);      
        Task<bool> AddProvider(Provider provider);
        Task<bool> EditProvider(Provider provider);
        Task<List<Transaction>?> GetTransactions();
        Task<List<Transaction>?> GetTransactionsByProvider(int providerId);
        Task<bool> CreateTransaction(PaymentRequest request);
        Task<bool> DeleteTransaction(string id);
    }
}
