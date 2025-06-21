using OrderManagementApp.Repositories;
using OrderManagementApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace OrderManagementApp.Services
{
    public class UserService
    {
        private readonly UserRepository UserRepository;

        public UserService(UserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public bool CreateUser(User user)
        {
            try
            {
                user.PasswordHash = HashPassword(user.PasswordHash, Encoding.UTF8.GetBytes(user.Username));
                return UserRepository.CreateUser(user);
            }
            catch (Exception)
            {
                throw new Exception("Failed to create user.");
            }
        }

        public User? Login(string username, string password)
        {
            try
            {
                password = HashPassword(password, Encoding.UTF8.GetBytes(username));
                return UserRepository.CheckLogin(username, password);
            }
            catch (Exception)
            {
                throw new Exception("Login failed due to a system error.");
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return UserRepository.GetAllUsers();
            }
            catch (Exception)
            {
                throw new Exception("Failed to retrieve user list.");
            }
        }

        private string HashPassword(string password, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string password, string hashedPassword, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hashToCompare = pbkdf2.GetBytes(32);
            string computedHash = Convert.ToBase64String(hashToCompare);
            return hashedPassword == computedHash;
        }
    }
}
