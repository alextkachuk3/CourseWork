using BookingWebService.Models;

namespace BookingWebService.Services.HotelService
{
    public class HotelService : IHotelService
    {
        private readonly DatabaseContext _dbContext;
        public HotelService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
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
