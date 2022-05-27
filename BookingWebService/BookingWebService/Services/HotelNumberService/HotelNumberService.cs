using BookingWebService.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public HotelNumber? GetHotelNumberById(int id)
        {
            return _dbContext.HotelNumbers.Where(h => h.Id.Equals(id)).Include(h => h.Hotel).Include(h => h.Hotel.User).FirstOrDefault();
        }

        public List<HotelNumber> GetRandomHotelNumbers(int hotelNumberCount)
        {
            if (_dbContext.HotelNumbers.Count() < hotelNumberCount)
            {
                //hotelNumberCount = _dbContext.HotelNumbers.Count();
                return _dbContext.HotelNumbers.Include(h => h.Hotel).Include(h => h.Images).ToList();
            }

            var random = new Random();
            HashSet<int> numbers = new HashSet<int>();
            while (numbers.Count < hotelNumberCount)
            {
                numbers.Add(random.Next(0, hotelNumberCount));
            }
            
            List<HotelNumber> result = new List<HotelNumber>();

            foreach (int i in numbers)
            {
                HotelNumber? hotelNumber = _dbContext.HotelNumbers.Find(i);
                if(hotelNumber != null)
                {
                    result.Add(hotelNumber);
                }                
            }

            return result;
        }

        public void RemoveHotelNumber(int id)
        {
            var hotelNumber = _dbContext.HotelNumbers.Where(h => h.Id.Equals(id)).FirstOrDefault();
            
            try
            {
                if(hotelNumber != null)
                {
                    _dbContext.HotelNumbers.Remove(hotelNumber);
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
