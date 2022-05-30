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

        public Hotel? GetHotelById(int Id)
        {
            return _dbContext.Hotels.Where(h => h.Id.Equals(Id)).Include(h => h.User).FirstOrDefault();
        }
    }
}
