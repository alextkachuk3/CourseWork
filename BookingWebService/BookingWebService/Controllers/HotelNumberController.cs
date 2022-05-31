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

        public HotelNumberController(IUserService userService, IHotelNumberService hotelNumberService, IHotelService hotelService)
        {
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

        [HttpPut("add_booking_order"), Authorize]
        public async Task<ActionResult<BookingOrder>> AddBookingOrder(BookingOrderDtoHO request)
        {

            BookingOrder bookingOrder = new BookingOrder();

            bookingOrder.Year = request.Year;
            bookingOrder.Month = request.Month;
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

        [HttpGet("get_booking_orders"), Authorize]
        public async Task<ActionResult<List<BookingOrderDto>>> GetBookingOrders()
        {
            var userId = _userService.GetId();
            var bookingOrders = _hotelNumberService.GetBookingOrdersList(userId);
            var result = new List<BookingOrderDto>();
            foreach (var bookingOrder in bookingOrders)
            {
                var bookingOrderDto = new BookingOrderDto();
                bookingOrderDto.FirstName = bookingOrder.FirstName;
                bookingOrderDto.LastName = bookingOrder.LastName;
                bookingOrderDto.Year = bookingOrder.Year;
                bookingOrderDto.Month = bookingOrder.Month;
                bookingOrderDto.Day = bookingOrder.Day;
                bookingOrderDto.Id = bookingOrder.Id;
                bookingOrderDto.HotelNumberId = bookingOrder.HotelNumber.Id;
                result.Add(bookingOrderDto);
            }    
            return Ok(result);
        }
    }
}
