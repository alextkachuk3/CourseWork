using BookingWebService.Models;
using System.Security.Claims;

namespace BookingWebService.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DatabaseContext _dbContext;

        public UserService(IHttpContextAccessor httpContextAccessor, DatabaseContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public void AddUser(User user)
        {
            try
            {
                _dbContext.Users?.Add(user);
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

        public User FindUser(string login)
        {
            return _dbContext.Users.Where(i => i.Login.Equals(login)).FirstOrDefault();
        }

        public int GetId()
        {
            var login = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                login = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }

            return _dbContext.Users.Where(i => i.Login.Equals(login)).FirstOrDefault().UserId;
        }

        public string GetLogin()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }

            return result;
        }
    }
}
