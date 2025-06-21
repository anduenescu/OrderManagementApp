using Microsoft.Data.SqlClient;
using OrderManagementApp.Models;

namespace OrderManagementApp.Repositories
{
    public class OrderRepository
    {
        private string _connectionString = "Data Source=DESKTOP-ALBJ45J\\SQLEXPRESS;Initial Catalog=OrderManagementPlatform;Integrated Security=True;Trust Server Certificate=True";
        private readonly IProductRepository ProductRepo;

        public OrderRepository(IProductRepository productRepo)
        {
            ProductRepo = productRepo;
        }

        public bool CreateOrder(Order order)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Orders (TotalPrice, Status, UserId) VALUES (@totalprice, @status, @userid)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@totalprice", order.TotalPrice);
                        command.Parameters.AddWithValue("@status", order.Status);
                        command.Parameters.AddWithValue("@userid", order.User.Id);

                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (SqlException)
            {
                
                throw new Exception("A database error occurred while creating the order.");
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occurred while creating the order.");
            }
        }
    }
}
