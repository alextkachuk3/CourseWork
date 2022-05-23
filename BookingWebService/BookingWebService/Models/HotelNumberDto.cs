namespace BookingWebService.Models
{
    public class HotelNumberDto
    {
        public int HotelId { get; set; }

        public int Price { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool IsFree { get; set; }
    }
}
