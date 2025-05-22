using System.Text.Json.Serialization;

namespace MiniPay_Contracts
{
    public class Provider
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]       
        public required string Name { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive {  get; set; }   
        
        [JsonPropertyName("endpointUrl")]
        public string? EndpointUrl { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("supportedCurrencies")]
        public IEnumerable<Currency>? CurrenciesAllowed { get; set; }
       
    }
}
