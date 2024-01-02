namespace TaxiDispatch.API.DataTransferObjects.OrderDto
{
    public class OrderPutDto
    {
        public int CustomerId { get; set; }

        public int DriverId { get; set; }

        public string PickupLocation { get; set; } = "";

        public string Destination { get; set; } = "";

        public DateTime? OrderDateTime { get; set; } = DateTime.Now;

        public string Status { get; set; } = "";    
    }
}
