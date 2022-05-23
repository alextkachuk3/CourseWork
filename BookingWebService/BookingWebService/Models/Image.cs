namespace BookingWebService.Models
{
    public class Image
    {
        public int Id { get; set; }

        public byte[]? ImageData { get; set; }

        List<HotelNumber> HotelNumbers { get; set; } = new List<HotelNumber>();
    }
}
