using BookingWebService.Models;
using BookingWebService.Services.HotelNumberService;
using BookingWebService.Services.ImageService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BookingWebService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        private readonly IHotelNumberService _hotelNumberService;

        public ImageController(IConfiguration configuration, IUserService userService, IImageService imageService, IHotelNumberService hotelNumberService)
        {
            _configuration = configuration;
            _userService = userService;
            _imageService = imageService;
            _hotelNumberService = hotelNumberService;
        }

        [HttpPost("add_image"), Authorize]
        public async Task<ActionResult<Image>> AddImage(ImageDto request)
        {
            HotelNumber? hotelNumber = _hotelNumberService.GetHotelNumberById(request.HotelNumberId);
            User? user = _userService.GetUser();

            if (hotelNumber == null)
            {
                return NotFound("Hotel number with id " + request.HotelNumberId + " does not exists");
            }

            if (hotelNumber.Hotel?.User?.Login != user?.Login)
            {
                return BadRequest("You are not owner of hotel number with id " + request.HotelNumberId);
            }

            var image = new Image();

            image.ImageData = Encoding.UTF8.GetBytes(request.Base64ImageData);
            image.User = user;
            image.HotelNumber = hotelNumber;

            _imageService.AddImage(image);

            return Ok(image);
        }

    }
}
