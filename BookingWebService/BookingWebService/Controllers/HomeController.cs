using BookingWebService.Services.HotelNumberService;
using Microsoft.AspNetCore.Mvc;

namespace BookingWebService.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHotelNumberService _hotelNumber;

        public HomeController(IHotelNumberService hotelNumber)
        {
            _hotelNumber = hotelNumber;
        }

        public IActionResult Index()
        {
            var hotelNumbers = _hotelNumber.GetRandomHotelNumbers(10);
            return View(hotelNumbers);
        }
    }
}
