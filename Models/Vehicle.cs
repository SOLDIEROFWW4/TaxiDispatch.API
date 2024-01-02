using System;
using System.Collections.Generic;

namespace TaxiDispatch.API.Models;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public int DriverId { get; set; }

    public string VehicleType { get; set; } = null!;

    public int Year { get; set; }

    public string Color { get; set; } = null!;

    public virtual Driver Driver { get; set; } = null!;
}
