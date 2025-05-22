using System.ComponentModel.DataAnnotations;

namespace MiniPay_Frontend
{
    public class TransactionValidation
    {
        public static ValidationResult? ValidateAmount(decimal? amount, ValidationContext context)
        {
            if (amount != null && amount.Value >= 10)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Minimum Amount of 10", ["Amount"]);
        }
    }
}
