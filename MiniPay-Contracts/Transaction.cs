using System.Text.Json.Serialization;

namespace MiniPay_Contracts
{
    public class Transaction
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("providerId")]
        public int? ProviderId { get; set; }

        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; }

        [JsonPropertyName("currency")]
        public Currency? Currency { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("status")]       
        public string? Status { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp {  get; set; }
        
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("referenceId")]
        public string? ReferenceId { get; set; }
       
    }
}
