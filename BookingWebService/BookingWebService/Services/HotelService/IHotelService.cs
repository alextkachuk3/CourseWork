using BookingWebService.Models;

namespace BookingWebService.Services.HotelService
{
    public interface IHotelService
    {
        public void AddHotel(Hotel hotel);

        public Hotel? GetHotelById(int HotelId);

    }
}
