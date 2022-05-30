namespace BookingWebService.Models
{
    public class BookingOrderDto
    {
        public int Id { get; set; }

        public int HotelNumberId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }
    }
}
