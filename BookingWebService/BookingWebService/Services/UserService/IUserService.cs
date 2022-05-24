using BookingWebService.Models;

namespace BookingWebService.Services.UserService
{
    public interface IUserService
    {
        string GetLogin();

        int GetId();

        public User? GetUser();

        public void AddUser(User user);

        public User? FindUser(string login);

    }
}
