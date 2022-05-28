using System.Text.Json.Serialization;

namespace BookingWebService.Models
{
    public class BookingOrder
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        [JsonIgnore]
        public HotelNumber? HotelNumber { get; set; }

    }
}
