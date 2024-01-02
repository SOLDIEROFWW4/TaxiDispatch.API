namespace TaxiDispatch.API.DataTransferObjects.RatingDto
{
    public class RatingGetDto
    {
        public int RatingId { get; set; }

        public int? OrderId { get; set; }

        public int? RatingValue { get; set; }

        public string? Comment { get; set; }
    }
}
