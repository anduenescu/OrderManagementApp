using OrderManagementApp.Repositories;
using OrderManagementApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace OrderManagementApp.Services
{
    public class UserService
    {
        private readonly UserRepository UserRepository;
        private readonly ProductService ProductService;

        public UserService(UserRepository userRepository, ProductService productService)
        {
            UserRepository = userRepository;
            ProductService = productService;
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
            try
            {
                using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
                byte[] hash = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hash);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error hashing password: " + ex.Message);
                throw new ApplicationException("Password hashing failed.");
            }
        }

        private bool VerifyPassword(string password, string hashedPassword, byte[] salt)
        {
            try
            {
                using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
                byte[] hashToCompare = pbkdf2.GetBytes(32);
                string computedHash = Convert.ToBase64String(hashToCompare);
                return hashedPassword == computedHash;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error verifying password: " + ex.Message);
                return false; // fallback: fail gracefully
            }
        }

        public bool AddToCart(int userId, int idProduct, int quantity)
        {
            try
            {
                //check if the product can be added to the cart
                if (!ProductService.IsInStock(idProduct, quantity))
                    return false;

                return UserRepository.addToCart(userId, idProduct, quantity);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add product to cart");

            }
        }
    }
}
