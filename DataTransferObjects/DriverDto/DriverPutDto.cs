namespace TaxiDispatch.API.DataTransferObjects.DriverDto
{
    public class DriverPutDto
    {
        public int UserId { get; set; }

        public string LicenseNumber { get; set; } = "";

        public string CarModel { get; set; } = "";

        public string CarPlateNumber { get; set; } = "";

        public bool IsAvailability { get; set; } 
    }
}
