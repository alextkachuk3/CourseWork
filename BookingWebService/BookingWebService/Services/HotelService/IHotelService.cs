using BookingWebService.Models;

namespace BookingWebService.Services.HotelService
{
    public interface IHotelService
    {
        public void AddHotel(Hotel hotel);

        public void RemoveHotel(int Id);

        public List<int> GetOwnHotelIdList();

        public Hotel? GetHotelById(int HotelId);

    }
}
