using System.Text.Json.Serialization;

namespace BookingWebService.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string ImageData { get; set; } = string.Empty;

        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public HotelNumber? HotelNumber { get; set; }
    }
}
