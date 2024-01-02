namespace TaxiDispatch.API.DataTransferObjects.VehicleDto
{
    public class VehiclePutDto
    {
        public int DriverId { get; set; }

        public string VehicleType { get; set; } = "";

        public int Year { get; set; }

        public string Color { get; set; } = "";
    }
}
