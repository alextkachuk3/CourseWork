using BookingWebService.Models;

namespace BookingWebService.Services.HotelNumberService
{
    public interface IHotelNumberService
    {
        public void AddHotelNumber(HotelNumber hotelNumber);

        public HotelNumber? GetHotelNumberById(int id);

        public List<HotelNumber> GetRandomHotelNumbers(int hotelNumberCount);

        public void RemoveHotelNumber(int id);

        public bool CheckHotelStatus(int id);

        public void UpdateHotelStatus(int id, bool status);

    }
}
