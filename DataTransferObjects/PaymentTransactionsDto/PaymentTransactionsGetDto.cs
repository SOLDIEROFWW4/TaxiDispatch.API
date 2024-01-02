namespace TaxiDispatch.API.DataTransferObjects.PaymentTransactionsDto
{
    public class PaymentTransactionsGetDto
    {
        public int TransactionId { get; set; }

        public int? OrderId { get; set; }

        public int? Amount { get; set; }

        public DateTime? PaymentDateTime { get; set; }

        public bool? IsPaymentCompleted { get; set; }
    }
}
