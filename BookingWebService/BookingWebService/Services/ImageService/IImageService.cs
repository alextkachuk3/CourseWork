using BookingWebService.Models;

namespace BookingWebService.Services.ImageService
{
    public interface IImageService
    {
        public void AddImage(Image image);

        public string? GetImage(int id);

        public string? GetFirstHotelNumberImage(int hotelNumberId);

    }
}
