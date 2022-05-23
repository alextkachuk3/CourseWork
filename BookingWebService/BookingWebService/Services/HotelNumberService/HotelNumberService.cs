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
            throw new NotImplementedException();
        }

        public bool CheckHotelStatus(int Id)
        {
            throw new NotImplementedException();
        }

        public void RemoveHotelNumber(int Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateHotelStatus(int Id, bool status)
        {
            throw new NotImplementedException();
        }
    }
}
