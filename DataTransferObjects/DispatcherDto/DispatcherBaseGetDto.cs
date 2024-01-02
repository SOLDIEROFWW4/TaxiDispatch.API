namespace TaxiDispatch.API.DataTransferObjects.DispatcherDto
{
    public class DispatcherBaseGetDto
    {
        public int DispatcherId { get; set; }

        public int? UserId { get; set; }

        public string? Department { get; set; }

        public int? ShiftTime { get; set; }
    }
}
