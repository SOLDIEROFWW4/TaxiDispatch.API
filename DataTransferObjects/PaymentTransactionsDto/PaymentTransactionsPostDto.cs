namespace TaxiDispatch.API.DataTransferObjects.PaymentTransactionsDto
{
    public class PaymentTransactionsPostDto
    {
        public int OrderId { get; set; }

        public int? Amount { get; set; } = null!;

        public DateTime? PaymentDateTime { get; set; } = null!;

        public bool? IsPaymentCompleted { get; set; } = null!;
    }
}
