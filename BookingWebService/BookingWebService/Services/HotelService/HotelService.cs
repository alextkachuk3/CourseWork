using BookingWebService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingWebService.Services.HotelService
{
    public class HotelService : IHotelService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IUserService _userService;
        public HotelService(DatabaseContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }
        public void AddHotel(Hotel hotel)
        {
            try
            {
                _dbContext.Hotels?.Add(hotel);
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbContext.SaveChanges();
            }
        }

        public Hotel GetHotelById(int Id)
        {
            return _dbContext.Hotels.Where(h => h.Id.Equals(Id)).Include(h => h.User).FirstOrDefault();
        }

        public List<int> GetOwnHotelIdList()
        {
            List<int> result = new List<int>();
            foreach (Hotel hotel in _dbContext.Hotels.Where(h => h.User.Id.Equals(_userService.GetId())))
                result.Add(hotel.Id);
            return result;
        }

        public void RemoveHotel(int Id)
        {
            try
            {
                var itemToRemove = _dbContext.Hotels.Where(i => i.Id.Equals(Id)).FirstOrDefault();

                if (itemToRemove != null)
                {
                    foreach (var hotelNumber in itemToRemove.HotelNumbers)
                    {
                        _dbContext.HotelNumbers.Remove(hotelNumber);
                    }
                        
                    _dbContext.Hotels.Remove(itemToRemove);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbContext.SaveChanges();
            }
        }
    }
}
