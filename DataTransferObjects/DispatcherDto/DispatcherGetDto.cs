namespace TaxiDispatch.API.DataTransferObjects.DispatcherDto
{
    public class DispatcherGetDto
    {
        public int DispatcherId { get; set; }

        public int? UserId { get; set; }

        public string? Department { get; set; }

        public int? ShiftTime { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }
}
