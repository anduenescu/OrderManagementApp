using Microsoft.Identity.Client;
using OrderManagementApp.Repositories;
using OrderManagementApp.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System.Text;
namespace OrderManagementApp.Services
{
    public class UserService
    {
        UserRepository UserRepository;
        public UserService(UserRepository userRepository)
        {
            UserRepository = userRepository;

        }
        public bool CreateUser(User user)
        {
            user.PasswordHash = HashPassword(user.PasswordHash, Encoding.UTF8.GetBytes(user.Username)); 
            return UserRepository.CreateUser(user);

        }

        private string HashPassword(string password, byte[] salt)
        {
            // Generate a cryptographic random salt
            //salt = RandomNumberGenerator.GetBytes(16); // 128-bit salt

            // Derive a 256-bit subkey (use 100,000 iterations)
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32); // 256-bit hash

            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string password, string hashedPassword, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hashToCompare = pbkdf2.GetBytes(32);

            string computedHash = Convert.ToBase64String(hashToCompare);
            return hashedPassword == computedHash;
        }

        public User? Login(string username, string password) 
        {
           password = HashPassword(password, Encoding.UTF8.GetBytes(username));
           return UserRepository.CheckLogin(username, password);

        }
    }
}
