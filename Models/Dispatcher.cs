namespace TaxiDispatch.API.Models;

public partial class Dispatcher
{
    public int DispatcherId { get; set; }

    public int UserId { get; set; }

    public string Department { get; set; } = null!;

    public int ShiftTime { get; set; }

    public virtual User User { get; set; } = null!;
}
