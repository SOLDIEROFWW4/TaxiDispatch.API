namespace TaxiDispatch.API.DataTransferObjects.DriverDto
{
    public class DriverGetDto
    {
        public int DriverId { get; set; }

        public int UserId { get; set; }

        public string? LicenseNumber { get; set; } = null!;

        public string? CarModel { get; set; } = null!;

        public string? CarPlateNumber { get; set; } = null!;

        public bool? IsAvailability { get; set; } = false!;

        public string? Username { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? Phone { get; set; } = null!;
    }
}
