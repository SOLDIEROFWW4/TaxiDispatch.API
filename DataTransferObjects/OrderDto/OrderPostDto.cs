namespace TaxiDispatch.API.DataTransferObjects.OrderDto
{
    public class OrderPostDto
    {
        public int CustomerId { get; set; }

        public int DriverId { get; set; }

        public string? PickupLocation { get; set; } = null!;

        public string? Destination { get; set; } = null!;

        public DateTime? OrderDateTime { get; set; } = null!;

        public string? Status { get; set; } = null!;
    }
}
