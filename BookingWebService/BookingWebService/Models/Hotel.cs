namespace BookingWebService.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }

        public string? Name { get; set; }

        public string? City { get; set; }

        public string? Address { get; set; }

        public int? UserId { get; set; }

        public List<HotelNumber>? HotelNumbers { get; set; }
    }
}
