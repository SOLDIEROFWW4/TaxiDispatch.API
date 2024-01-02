namespace TaxiDispatch.API.DataTransferObjects.CustomerDto
{
    public class CustomerPutDto
    {
        public int UserId { get; set; }

        public string Address { get; set; } = "";
    }
}
