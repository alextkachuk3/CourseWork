using BookingWebService.Controllers;
using BookingWebService.Models;
using BookingWebService.Services.HotelService;
using BookingWebService.Services.UserService;
using FakeItEasy;

namespace BookingService.Tests
{
    public class HotelControllerTests
    {
        [Fact]
        public void Test()
        {
            var hotelService = A.Fake<IHotelService>();
            var hotel = A.Fake<Hotel>();
            A.CallTo(() => hotelService.AddHotel(hotel));
            var userService = A.Fake<IUserService>();
            var controller = new HotelController(userService, hotelService);
            Assert.Equal(1, 1);

        }
    }
}