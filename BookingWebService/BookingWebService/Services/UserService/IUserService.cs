using BookingWebService.Models;

namespace JwtWebApiTutorial.Services.UserService
{
    public interface IUserService
    {
        string GetLogin();

        public void AddUser(User user);

        public User FindUser(string login);
    }
}
