using System.Text.Json.Serialization;

namespace BookingWebService.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public List<HotelNumber>? HotelNumbers { get; set; } = new List<HotelNumber>();
    }
}
