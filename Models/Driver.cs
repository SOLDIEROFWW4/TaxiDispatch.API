using System;
using System.Collections.Generic;

namespace TaxiDispatch.API.Models;

public partial class Driver
{
    public int DriverId { get; set; }

    public int UserId { get; set; }

    public string LicenseNumber { get; set; } = null!;

    public string CarModel { get; set; } = null!;

    public string CarPlateNumber { get; set; } = null!;

    public ulong IsAvailability { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
