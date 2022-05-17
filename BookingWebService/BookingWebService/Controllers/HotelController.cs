﻿using BookingWebService.Models;
using BookingWebService.Services.HotelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IHotelService _hotelService;

        public HotelController(IConfiguration configuration, IUserService userService, IHotelService hotelService)
        {
            _configuration = configuration;
            _userService = userService;
            _hotelService = hotelService;
        }

        [HttpPost("add_hotel"), Authorize]
        public async Task<ActionResult<Hotel>> AddHotel(HotelDto request)
        {
            var hotel = new Hotel();

            hotel.Name = request.Name;
            hotel.City = request.City;
            hotel.Address = request.Address;
            hotel.UserId = _userService.GetId();
            hotel.HotelNumbers = new List<HotelNumber>();

            _hotelService.AddHotel(hotel);

            return Ok(hotel);
        }
    }
}
