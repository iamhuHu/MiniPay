using MiniPay_Contracts;
using System.ComponentModel.DataAnnotations;

namespace MiniPay_Frontend.Models
{
    public class ProviderModel
    {
        public int Id { get; set; }
        
        [Required]
        public required string Name { get; set; }
      
        public bool IsActive {  get; set; }

        [Required]
        public string? EndpointUrl { get; set; }

        public string? Description { get; set; }

        public IEnumerable<Currency>? CurrenciesAllowed { get; set; }
    }
}
