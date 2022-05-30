namespace BookingWebService.ViewModels
{
    public class DetailedHotelNumberViewModel
    {
        public int Id { get; set; }

        public string? HotelName { get; set; }

        public string? description { get; set; }

        public List<string?>? booking_orders { get; set; }

        public List<string?>? Images { get; set; }

    }
}
