using CarRentalApi.Models;

namespace CarRentalApi.Interface
{
    public interface IAuthentication
    {
        Task<User> Login(string username, string password);
        Task<User> Add(User user);
    }
}
