using MiniPay_Contracts;
using System.Text.Json;

namespace MiniPay_Backend
{
    public static class TransactionStorage
    {
        private const string TransactionsFilePath = @"data/transactions.json";

        public static List<Transaction>? GetStoredTransactions()
        {
            var jsonContent = FileStorage.ReadFile(TransactionsFilePath);
            var transactions = !string.IsNullOrEmpty(jsonContent) ? JsonSerializer.Deserialize<List<Transaction>>(jsonContent) : [];
            return transactions;
        }

        public static void StoreTransactions(List<Transaction> transactions)
        {
            FileStorage.WriteFile(TransactionsFilePath, JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
