using Microsoft.Data.SqlClient;
using OrderManagementApp.Models;

namespace OrderManagementApp.Repositories
{
    public class UserRepository
    {
        private string _connectionString = "Data Source=DESKTOP-ALBJ45J\\SQLEXPRESS;Initial Catalog=OrderManagementPlatform;Integrated Security=True;Trust Server Certificate=True";

        public bool CreateUser(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Users (UserName, UserPassword, Role) VALUES (@username, @password, @role)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", user.Username);
                        command.Parameters.AddWithValue("@password", user.PasswordHash);
                        command.Parameters.AddWithValue("@role", user.Role ?? "User");

                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while creating the user.");
            }
        }

        public User? CheckLogin(string username, string password)
        {
            try
            {
                User result = null;

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Users WHERE UserName = @UserName AND UserPassword = @UserPassword";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", username);
                        command.Parameters.AddWithValue("@UserPassword", password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = new User
                                {
                                    Id = reader.GetInt32(0),
                                    Username = reader.GetString(1),
                                    PasswordHash = reader.GetString(2),
                                    Role = reader.GetString(3)
                                };
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while checking user login.");
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                List<User> users = new();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Users";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                Username = reader.IsDBNull(1) ? "Unknown" : reader.GetString(1),
                                PasswordHash = reader.IsDBNull(2) ? "Unknown" : reader.GetString(2),
                                Role = reader.IsDBNull(3) ? "User" : reader.GetString(3)
                            };

                            users.Add(user);
                        }
                    }
                }

                return users;
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while retrieving all users.");
            }
        }

        internal bool addToCart(int userId, int idProduct, int quantity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                   
                        string insertCartItemQuery = @"
                    INSERT INTO CartItems (ProductId, UserId, Quantity)
                    VALUES (@productId, @userId, @quantity)";

                        using (SqlCommand itemCmd = new SqlCommand(insertCartItemQuery, connection))
                        {
                            itemCmd.Parameters.AddWithValue("@productId", idProduct);
                            itemCmd.Parameters.AddWithValue("@userId", userId);
                            itemCmd.Parameters.AddWithValue("@quantity", quantity);

                            itemCmd.ExecuteNonQuery();
                        }
                   
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating order: " + ex.Message);
                return false;
            }
        }
    }
}
