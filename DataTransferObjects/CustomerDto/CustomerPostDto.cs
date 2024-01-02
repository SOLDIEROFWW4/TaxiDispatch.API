namespace TaxiDispatch.API.DataTransferObjects.CustomerDto
{
    public class CustomerPostDto
    {
        public int UserId { get; set; }

        public string? Address { get; set; } = null!;
    }
}
