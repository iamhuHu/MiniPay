namespace MiniPay_Contracts
{
    public class PaymentRequest
    {
        public required int ProviderId { get; set; }
        public required decimal Amount { get; set; }
        public required Currency Currency {  get; set; }
        public string? Description { get; set; }
        public required string ReferenceId { get; set; }
    }
}
