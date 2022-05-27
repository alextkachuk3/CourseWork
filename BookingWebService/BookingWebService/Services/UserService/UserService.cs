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
            if(_dbContext.Users.Where(u => u.Login.Equals(user.Login)).Count() > 0)
            {
                throw new Exception("User with login " + user.Login + " already exists");
            }
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

        public User? FindUser(string? login)
        {
            return _dbContext.Users.Where(i => i.Login.Equals(login)).First();
        }

        public int GetId()
        {
            var login = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                login = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }

            User? user = _dbContext.Users.Where(i => i.Login.Equals(login)).FirstOrDefault();

            if (user == null)
            {
                return 0;
            }
            else
            {
                return user.Id;
            }
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

        public User? GetUser()
        {
            return _dbContext.Users.Where(i => i.Login.Equals(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name))).FirstOrDefault();
        }
    }
}
