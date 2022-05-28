using System.Text.Json.Serialization;

namespace BookingWebService.Models
{
    public class BookingOrderDtoHO
    {
        public int HotelNumberId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

    }
}
