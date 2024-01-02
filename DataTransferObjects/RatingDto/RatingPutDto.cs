namespace TaxiDispatch.API.DataTransferObjects.RatingDto
{
    public class RatingPutDto
    {
        public int OrderId { get; set; }

        public int RatingValue { get; set; }

        public string Comment { get; set; } = "";
    }
}
