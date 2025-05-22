using MiniPay_Contracts;
using System.Text;
using System.Text.Json;

namespace MiniPay_PaymentClient
{
    public class PaymentClient(string clientUrl) : IPaymentApi
    {
        private readonly HttpClient _client = new()
        {           
            BaseAddress = new Uri(clientUrl)
        };     

        public async Task<List<Provider>?> GetProviders()
        {
            var response = await _client.GetAsync("/getProviders");
            var responseString = await response.Content.ReadAsStringAsync();           
            return JsonSerializer.Deserialize<List<Provider>>(responseString);
        }

        public async Task<Provider?> GetProvider(int providerId)
        {
            var response = await _client.GetAsync($"/getProvider?providerId={providerId}");
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Provider>(responseString);
        }

        public async Task<bool> AddProvider(Provider provider)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(provider), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/addProvider", stringContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EditProvider(Provider provider)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(provider), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/editProvider", stringContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Transaction>?> GetTransactions()
        {
            var response = await _client.GetAsync("/getTransactions");
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Transaction>>(responseString);
        }

        public async Task<List<Transaction>?> GetTransactionsByProvider(int providerId)
        {
            var response = await _client.GetAsync($"/getTransactions?providerId={providerId}");
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Transaction>>(responseString);
        }

        public async Task<bool> CreateTransaction(PaymentRequest request)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/doTransaction", stringContent);
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(responseBody);
        }

        public async Task<bool> DeleteTransaction(string id)
        {
            var response = await _client.PostAsync($"/deleteTransaction?id={id}", null);
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(responseBody);
        }
    }
}
