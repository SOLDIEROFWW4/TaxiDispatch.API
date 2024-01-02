using System;
using System.Collections.Generic;

namespace TaxiDispatch.API.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Dispatcher> Dispatchers { get; set; } = new List<Dispatcher>();

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
}
