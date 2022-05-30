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
            return View();
        }

        public IActionResult Index()
        {
            var hotelNumbersView = new List<HotelNumberViewModel>(); 
            var hotelNumbers = _hotelNumber.GetRandomHotelNumbers(6);
            
            for(int i = 0; i < hotelNumbers.Count; i++)
            {
                var hotelNumber = new HotelNumberViewModel();
                hotelNumber.HotelName = hotelNumbers[i].Hotel.Name;
                hotelNumber.Id = hotelNumbers[i].Id;
                hotelNumber.Image = _imageService.GetFirstHotelNumberImage(hotelNumber.Id);
                hotelNumbersView.Add(hotelNumber);
            }
            

            return View(hotelNumbersView);
        }
    }
}
