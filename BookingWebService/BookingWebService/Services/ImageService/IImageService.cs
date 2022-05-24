using BookingWebService.Models;

namespace BookingWebService.Services.ImageService
{
    public interface IImageService
    {
        public void AddImage(Image image);

        public byte[]? GetImage(int id);

    }
}
