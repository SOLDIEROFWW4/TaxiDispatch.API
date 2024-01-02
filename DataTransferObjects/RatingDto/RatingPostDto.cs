namespace TaxiDispatch.API.DataTransferObjects.RatingDto
{
    public class RatingPostDto
    {
        public int OrderId { get; set; }

        public int RatingValue { get; set; }

        public string? Comment { get; set; } = null!;
    }
}
