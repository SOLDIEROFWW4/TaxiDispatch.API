namespace TaxiDispatch.API.DataTransferObjects.VehicleDto
{
    public class VehiclePostDto
    {
        public int DriverId { get; set; }

        public string? VehicleType { get; set; } = null!;

        public int? Year { get; set; } = null!;

        public string? Color { get; set; } = null!;
    }
}
