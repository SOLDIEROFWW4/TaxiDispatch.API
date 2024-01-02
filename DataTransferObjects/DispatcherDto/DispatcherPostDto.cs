namespace TaxiDispatch.API.DataTransferObjects.DispatcherDto
{
    public class DispatcherPostDto
    {
        public int? UserId { get; set; } = null!;

        public string? Department { get; set; } = null!;

        public int? ShiftTime { get; set; } = null!;
    }
}
