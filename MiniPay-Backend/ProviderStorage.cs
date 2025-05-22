using MiniPay_Contracts;
using System.Text.Json;

namespace MiniPay_Backend
{
    public static class ProviderStorage
    {
        private const string ProviderFilePath = @"data/providers.json";

        public static List<Provider>? GetStoredProviders()
        {
            var jsonContent = FileStorage.ReadFile(ProviderFilePath);
            var providers = !string.IsNullOrEmpty(jsonContent) ? JsonSerializer.Deserialize<List<Provider>>(jsonContent) : [];
            return providers;
        }

        public static void StoreProviders(List<Provider> providers)
        {
            FileStorage.WriteFile(ProviderFilePath, JsonSerializer.Serialize(providers, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
