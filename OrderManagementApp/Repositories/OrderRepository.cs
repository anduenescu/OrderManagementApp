using Microsoft.Data.SqlClient;
using OrderManagementApp.Models;

namespace OrderManagementApp.Repositories
{
    public class OrderRepository
    {
        private string _connectionString = "Data Source=DESKTOP-ALBJ45J\\SQLEXPRESS;Initial Catalog=OrderManagementPlatform;Integrated Security=True;Trust Server Certificate=True";
        ProductRepository ProductRepo;
        public OrderRepository(ProductRepository productRepo)
        {
            ProductRepo = productRepo;
        }

        public bool CreateOrder(Order order) //added to database 
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "insert into Orders (TotalPrice, Status, UserId) values (@totalprice, @status, @userid)";
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

        // get all the order by id or get all the order from a user by ID
        // get all order by admin 
    }
}
