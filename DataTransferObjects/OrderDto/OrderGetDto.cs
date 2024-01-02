namespace TaxiDispatch.API.DataTransferObjects.OrderDto
{
    public class OrderGetDto
    {
        public int OrderId { get; set; }

        public int? CustomerId { get; set; }

        public int? DriverId { get; set; }

        public string? PickupLocation { get; set; }

        public string? Destination { get; set; }

        public DateTime? OrderDateTime { get; set; }

        public string? Status { get; set; }
    }
}
