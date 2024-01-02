using System;
using System.Collections.Generic;

namespace TaxiDispatch.API.Models;

public partial class Rating
{
    public int RatingId { get; set; }

    public int OrderId { get; set; }

    public int RatingValue { get; set; }

    public string Comment { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
