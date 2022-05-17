namespace BookingWebService.Models
{
    public class HotelNumber
    {
        public int HotelNumberId { get; set; }

        public string? Name { get; set; }

        public byte[]? ImagesId { get; set; }

        public bool IsFree { get; set; }

        public int UserId { get; set; }

        public Hotel? Hotel { get; set; }
    }
}
