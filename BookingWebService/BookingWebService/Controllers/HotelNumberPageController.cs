using Microsoft.AspNetCore.Mvc;

namespace BookingWebService.Controllers
{
    public class HotelNumberPageController : Controller
    {
        public IActionResult HotelNumber()
        {
            return View();
        }
    }
}
