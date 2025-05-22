using MiniPay_Contracts;

namespace MiniPay_Backend.Endpoints
{    
    public static class ProviderEndpoints
    {
        private const string EndpointTag = "Provider";        

        public static void UseProviderEndpoints(this WebApplication app)
        {
            app.MapGet("/getProviders", () 
                => GetProviders()).WithTags(EndpointTag);

            app.MapGet("/getProvider", (int providerId)
                => GetProvider(providerId)).WithTags(EndpointTag);

            app.MapPost("/addProvider", (Provider provider) 
                => AddProvider(provider)).WithTags(EndpointTag);

            app.MapPost("/editProvider", (Provider provider) 
                => EditProvider(provider)).WithTags(EndpointTag);
        }
        
        private static List<Provider>? GetProviders()
        {
            return ProviderStorage.GetStoredProviders();
        }

        private static Provider? GetProvider(int providerId)
        {
            var providers = ProviderStorage.GetStoredProviders();
            return providers?.SingleOrDefault(d => d.Id == providerId);
        }      

        private static IResult AddProvider(Provider provider)
        {
            var providers = ProviderStorage.GetStoredProviders();
            if (providers != null)
            {
                // simulate next Id in case of no DB usage
                int maxId = providers.Count == 0 ? 0 : providers.Max(d => d.Id);
                provider.Id = ++maxId;
                providers.Add(provider);

                ProviderStorage.StoreProviders(providers);
            }
            return Results.Ok(true);
        }

        private static IResult EditProvider(Provider provider)
        {
            var providers = ProviderStorage.GetStoredProviders();
            if (providers != null)
            {
                var curProvider = providers.SingleOrDefault(d => d.Id == provider.Id);
                if (curProvider != null)
                {
                    curProvider.CurrenciesAllowed = provider.CurrenciesAllowed;
                    curProvider.Name = provider.Name;
                    curProvider.EndpointUrl = provider.EndpointUrl;
                    curProvider.Description = provider.Description;
                    curProvider.IsActive = provider.IsActive;

                    ProviderStorage.StoreProviders(providers);
                }
            }
            return Results.Ok(true);
        }
    }
}