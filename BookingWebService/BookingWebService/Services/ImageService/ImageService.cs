using BookingWebService.Models;

namespace BookingWebService.Services.ImageService
{
    public class ImageService : IImageService
    {
        private readonly DatabaseContext _dbContext;
        public ImageService(DatabaseContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
        }
        public void AddImage(Image image)
        {
            try
            {
                _dbContext.Images?.Add(image);
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbContext.SaveChanges();
            }
        }

        public string? GetFirstHotelNumberImage(int hotelNumberId)
        {
            return _dbContext.Images.Where(i => i.HotelNumber.Id.Equals(hotelNumberId)).FirstOrDefault().ImageData;
        }

        public string? GetImage(int id)
        {
            Image? image = _dbContext.Images.Where(i => i.Id.Equals(id)).FirstOrDefault();
            if(image == null)
            {
                return null;
            }
            else
            {
                return image.ImageData;
            }            
        }

    }
}
