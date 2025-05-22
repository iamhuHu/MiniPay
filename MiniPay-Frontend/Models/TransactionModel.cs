using MiniPay_Contracts;
using System.ComponentModel.DataAnnotations;

namespace MiniPay_Frontend.Models
{
    public class TransactionModel
    {
        [Required]
        [CustomValidation(typeof(TransactionValidation), "ValidateAmount")]
        public decimal Amount { get; set; }

        public Currency Currency { get; set; }
      
        public string? Description { get; set; }
       
        public int SelectedProvider { get; set; }

        public List<Provider>? AllowedProviders { get; set; } 
    }
}
