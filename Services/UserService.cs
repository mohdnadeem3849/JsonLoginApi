using System.Text.Json;
using JsonLoginApi.Models;

namespace JsonLoginApi.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users;

        public UserService()
        {
            // Build the path to users.json in the output folder
            var basePath = AppContext.BaseDirectory;
            var usersFilePath = Path.Combine(basePath, "Data", "users.json");

            if (!File.Exists(usersFilePath))
            {
                // You might want better error handling in real life
                throw new FileNotFoundException($"users.json not found at {usersFilePath}");
            }

            var json = File.ReadAllText(usersFilePath);

            _users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        public User? ValidateUser(string userId, string password)
        {
            // Simple comparison (plaintext) – ONLY FOR DEMO!
            return _users.FirstOrDefault(u =>
                string.Equals(u.UserId, userId, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);
        }
    }
}
