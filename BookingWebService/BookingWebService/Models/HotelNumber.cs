using System.Text.Json.Serialization;

namespace BookingWebService.Models
{
    public class HotelNumber
    {
        public int Id { get; set; }

        [JsonIgnore]
        public int Price { get; set; }

        [JsonIgnore]
        public string Description { get; set; } = string.Empty;

        [JsonIgnore]
        public bool IsFree { get; set; }

        [JsonIgnore]
        public Hotel? Hotel { get; set; }

        [JsonIgnore]
        public List<Image> Images { get; set; } = new List<Image>();
    }
}
