using Azure.Identity;
using Microsoft.Data.SqlClient;
using OrderManagementApp.Models;

namespace OrderManagementApp.Repositories
{
    public class UserRepository
    {
        private string _connectionString = "Data Source=DESKTOP-ALBJ45J\\SQLEXPRESS;Initial Catalog=OrderManagementPlatform;Integrated Security=True;Trust Server Certificate=True";
        public bool CreateUser(User user) //added to database 
        {
            
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "insert into Users (UserName, UserPassword) values (@username, @password)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", user.Username);
                    command.Parameters.AddWithValue("@password", user.PasswordHash);

                    command.ExecuteNonQuery();
                }
            }
            return true;
        }
        public User? CheckLogin(string username, string password)
        {
            User result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                connection.Open();

                string query = "select * from Users WHERE UserName = @UserName and UserPassword = @UserPassword";
                
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
                                PasswordHash = reader.GetString(2)
                            };

                        }
                    }
             
                }

            }
            return result;

        }
    }


}




// a new funtion to check the password - for a specific user u send back the hash of the user and you compare if the two hashes are the same 