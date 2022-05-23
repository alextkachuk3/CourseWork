using BookingWebService.Models;

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

        public List<int> GetOwnHotelIdList()
        {
            List<int> result = new List<int>();
            foreach (Hotel hotel in _dbContext.Hotels.Where(i => i.UserId.Equals(_userService.GetId())))
                result.Add(hotel.HotelId);
            return result;
        }

        public void RemoveHotel(int Id)
        {
            try
            {
                var item_to_remove = _dbContext.Hotels?.Where(i => i.HotelId.Equals(Id)).FirstOrDefault();

                if (item_to_remove != null)
                {
                    _dbContext.Hotels?.Remove(item_to_remove);
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
