using BookingWebService.Models;

namespace BookingWebService.Services.HotelNumberService
{
    public interface IHotelNumberService
    {
        public void AddHotelNumber(HotelNumber hotelNumber);
        public void RemoveHotelNumber(int Id);
        public bool CheckHotelStatus(int Id);
        public void UpdateHotelStatus(int Id, bool status);
    }
}
