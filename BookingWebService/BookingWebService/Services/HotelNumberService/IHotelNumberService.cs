using BookingWebService.Models;

namespace BookingWebService.Services.HotelNumberService
{
    public interface IHotelNumberService
    {
        public void AddHotelNumber(HotelNumber hotelNumber);

        public HotelNumber? GetHotelNumberById(int hotelNumberId);

        public List<HotelNumber> GetRandomHotelNumbers(int hotelNumberCount);

        public void RemoveHotelNumber(int hotelNumberId);

        public void AddBookingOrder(int hotelNumberId, BookingOrder bookingOrder);

        public List<BookingOrder> GetBookingOrdersList(int ownerId);

    }
}
