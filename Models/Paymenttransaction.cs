using System;
using System.Collections.Generic;

namespace TaxiDispatch.API.Models;

public partial class Paymenttransaction
{
    public int TransactionId { get; set; }

    public int OrderId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDateTime { get; set; }

    public ulong IsPaymentCompleted { get; set; }

    public virtual Order Order { get; set; } = null!;
}
