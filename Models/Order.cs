using System;
using System.Collections.Generic;

namespace TaxiDispatch.API.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public int? DriverId { get; set; }

    public string PickupLocation { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public DateTime OrderDateTime { get; set; }

    public string Status { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Driver? Driver { get; set; }

    public virtual ICollection<Paymenttransaction> Paymenttransactions { get; set; } = new List<Paymenttransaction>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
