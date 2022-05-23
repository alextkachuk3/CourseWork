using BookingWebService.Models;

namespace BookingWebService.Services.HotelNumberService
{
    public class HotelNumberService : IHotelNumberService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IUserService _userService;
        public HotelNumberService(DatabaseContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }
        public void AddHotelNumber(HotelNumber hotelNumber)
        {
            try
            {
                _dbContext.HotelNumbers?.Add(hotelNumber);
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

        public bool CheckHotelStatus(int id)
        {
            var hotelNumber = _dbContext.HotelNumbers?.Where(h => h.Id.Equals(id)).FirstOrDefault();

            if(hotelNumber == null)
            {
                return false;
            }
            else
            {
                return hotelNumber.IsFree;
            }
                
        }

        public void RemoveHotelNumber(int id)
        {
            var hotelNumber = _dbContext.HotelNumbers.Where(h => h.Id.Equals(id)).FirstOrDefault();
            
            try
            {
                if(hotelNumber != null)
                {
                    _dbContext.HotelNumbers.Remove(hotelNumber);
                    // hotelNumber?.Hotel?.HotelNumbers.Remove(hotelNumber);
                    
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

        public void UpdateHotelStatus(int id, bool status)
        {
            var hotelNumber = _dbContext.HotelNumbers.Where(h => h.Id.Equals(id)).FirstOrDefault();

            try
            {
                if(hotelNumber != null)
                {
                    hotelNumber.IsFree = status;
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
