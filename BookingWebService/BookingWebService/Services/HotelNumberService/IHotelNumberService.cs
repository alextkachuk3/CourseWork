using BookingWebService.Models;

namespace BookingWebService.Services.HotelNumberService
{
    public interface IHotelNumberService
    {
        public void AddHotelNumber(HotelNumber hotelNumber);
        public void RemoveHotelNumber(int id);
        public bool CheckHotelStatus(int id);
        public void UpdateHotelStatus(int id, bool status);
    }
}
