using BookingWebService.Models;
using BookingWebService.Services.HotelNumberService;
using BookingWebService.Services.ImageService;
using BookingWebService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingWebService.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHotelNumberService _hotelNumber;
        private readonly IImageService _imageService;

        public HomeController(IHotelNumberService hotelNumber, IImageService imageService)
        {
            _hotelNumber = hotelNumber;
            _imageService = imageService;
        }

        //public string Number(int id) => $"Number Id: {id}";

        public IActionResult Number(int id)
        {
            var hotelNumber = _hotelNumber.GetHotelNumberById(id);

            return View(hotelNumber);
        }

        public IActionResult Index()
        {
            var hotelNumbersView = new List<HotelNumberViewModel>();
            var hotelNumbers = _hotelNumber.GetRandomHotelNumbers(6);

            for (int i = 0; i < hotelNumbers.Count; i++)
            {
                var hotelNumber = new HotelNumberViewModel();
                hotelNumber.HotelName = hotelNumbers[i].Hotel.Name;
                hotelNumber.Id = hotelNumbers[i].Id;
                hotelNumber.Image = _imageService.GetFirstHotelNumberImage(hotelNumber.Id);
                hotelNumbersView.Add(hotelNumber);
            }


            return View(hotelNumbersView);
        }

        [HttpPost]
        public IActionResult ProcessBooking(int hotelNumberId, string firstName, string lastName, int countOfDaysFromToday)
        {
            var date = DateTime.Now;
            date = date.AddDays(countOfDaysFromToday);
            BookingOrder? bookingOrder = new BookingOrder();
            bookingOrder.Year = date.Year;
            bookingOrder.Month = date.Month;
            bookingOrder.Day = date.Day;
            bookingOrder.FirstName = firstName;
            bookingOrder.LastName = lastName;

            try
            {
                _hotelNumber.AddBookingOrder(hotelNumberId, bookingOrder);
            }
            catch
            {
                bookingOrder = null;
            }
            if (bookingOrder == null)
            {
                ViewBag.Message = "Booking process failed!";

            }
            else
            {
                ViewBag.Message = "Success! Number of your booking order: " + bookingOrder.Id;
            }

            return View();
        }
    }
}
