namespace TaxiDispatch.API.DataTransferObjects.DriverDto
{
    public class DriverPostDto
    {
        public int UserId { get; set; }

        public string? LicenseNumber { get; set; } = null!;

        public string? CarModel { get; set; } = null!;

        public string? CarPlateNumber { get; set; } = null!;

        public bool? IsAvailability { get; set; } = null!;
    }
}
