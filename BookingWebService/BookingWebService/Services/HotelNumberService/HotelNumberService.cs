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

        public void AddBookingOrder(int hotelNumberId, BookingOrder bookingOrder)
        {
            var hotelNumber = _dbContext.HotelNumbers.Where(h => h.Id.Equals(hotelNumberId)).Include(h => h.BookingOrders).FirstOrDefault();

            if (hotelNumber == null)
            {
                throw new Exception("There is no hotel room with ID " + hotelNumberId);
            }

            if (hotelNumber.BookingOrders.Where(
                    o =>
                    o.Year.Equals(bookingOrder.Year) &&
                    o.Month.Equals(bookingOrder.Month) &&
                    o.Day.Equals(bookingOrder.Day)).FirstOrDefault() != null)
            {
                throw new Exception("A booking order already exists for this day!");
            }

            try
            {
                bookingOrder.HotelNumber = hotelNumber;
                _dbContext.BookingOrders?.Add(bookingOrder);
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

        public List<BookingOrder> GetBookingOrdersList(int ownerId)
        {
            return _dbContext.BookingOrders.Where(o => o.HotelNumber.Hotel.User.Id.Equals(ownerId)).Include(o => o.HotelNumber).ToList();
        }

        public HotelNumber? GetHotelNumberById(int hotelNumberId)
        {
            return _dbContext.HotelNumbers.Where(h => h.Id.Equals(hotelNumberId)).Include(h => h.Hotel).ThenInclude(h => h.User).Include(h => h.Images).Include(h => h.BookingOrders).FirstOrDefault();
        }

        public List<HotelNumber> GetRandomHotelNumbers(int hotelNumberCount)
        {
            var hotelCount = _dbContext.HotelNumbers.Count();

            if (hotelCount < hotelNumberCount)
            {
                return _dbContext.HotelNumbers.Include(h => h.Hotel).ToList();
            }

            var random = new Random();
            HashSet<int> numbers = new HashSet<int>();

            while (numbers.Count < hotelNumberCount)
            {
                    numbers.Add(random.Next(0, hotelCount));
            }            
            
            List<HotelNumber> result = new List<HotelNumber>();            

            foreach (int i in numbers)
            {
                HotelNumber? hotelNumber = _dbContext.HotelNumbers.Where(h => h.Id.Equals(i)).Include(h => h.Hotel).FirstOrDefault();
                if (hotelNumber != null)
                {
                    result.Add(hotelNumber);
                }                
            }

            return result;
        }

        

        public void RemoveHotelNumber(int hotelNumberId)
        {
            var hotelNumber = _dbContext.HotelNumbers.Where(h => h.Id.Equals(hotelNumberId)).FirstOrDefault();
            
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

    }
}
