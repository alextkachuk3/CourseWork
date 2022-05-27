using BookingWebService.Controllers.Models;
using BookingWebService.Models;
using BookingWebService.Services.HotelNumberService;
using BookingWebService.Services.HotelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingWebService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HotelNumberController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IHotelNumberService _hotelNumberService;
        private readonly IHotelService _hotelService;

        public HotelNumberController(IConfiguration configuration, IUserService userService, IHotelNumberService hotelNumberService, IHotelService hotelService)
        {
            _configuration = configuration;
            _userService = userService;
            _hotelNumberService = hotelNumberService;
            _hotelService = hotelService;
        }

        [HttpPost("add_hotel_number"), Authorize]
        public async Task<ActionResult<HotelNumber>> AddHotelNumber(HotelNumberDto request)
        {
            Hotel? hotel = _hotelService.GetHotelById(request.HotelId);

            if(hotel == null)
            {
                return NotFound("Hotel with id " + request.HotelId + " does not exists");
            }
            
            if(hotel.User?.Login != _userService.GetLogin())
            {
                return BadRequest("You are not owner of hotel with id " + request.HotelId);
            }

            var hotelNumber = new HotelNumber();

            hotelNumber.Hotel = hotel;
            hotelNumber.Price = request.Price;
            hotelNumber.Description = request.Description;

            _hotelNumberService.AddHotelNumber(hotelNumber);

            return Ok(hotelNumber);
        }

        [HttpPut("update_booking_orders"), Authorize]
        public async Task<ActionResult<BookingOrder>> AddBookingOrder(BookingOrderDtoHO request)
        {

            BookingOrder bookingOrder = new BookingOrder();

            bookingOrder.Year = request.Year;
            bookingOrder.Mounth = request.Mounth;
            bookingOrder.Day = request.Day;

            try
            {
                _hotelNumberService.AddBookingOrder(request.HotelNumberId, bookingOrder);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(bookingOrder);
        }
    }
}
