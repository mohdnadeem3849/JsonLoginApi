using JsonLoginApi.Models;

namespace JsonLoginApi.Services
{
    public interface IUserService
    {
        User? ValidateUser(string userId, string password);
    }
}
