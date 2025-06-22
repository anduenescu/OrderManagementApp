using Microsoft.Data.SqlClient;
using OrderManagementApp.Models;
using OrderManagementApp.Models.DTOs;

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

                    // Step 1: Insert order and get its generated ID
                    string insertOrderQuery = @"
                INSERT INTO Orders (TotalPrice, Status, UserId)
                OUTPUT INSERTED.OrderId
                VALUES (@totalprice, @status, @userid)";

                    int newOrderId;
                    using (SqlCommand orderCmd = new SqlCommand(insertOrderQuery, connection))
                    {
                        orderCmd.Parameters.AddWithValue("@totalprice", order.TotalPrice);
                        orderCmd.Parameters.AddWithValue("@status", order.Status);
                        orderCmd.Parameters.AddWithValue("@userid", order.User.Id);

                        newOrderId = (int)orderCmd.ExecuteScalar();
                    }

                    // Step 2: Insert each CartItem tied to the order
                    foreach (var item in order.Items)
                    {
                        string insertCartItemQuery = @"
                    INSERT INTO CartItems (ProductId, OrderId, Quantity)
                    VALUES (@productId, @orderId, @quantity)";

                        using (SqlCommand itemCmd = new SqlCommand(insertCartItemQuery, connection))
                        {
                            itemCmd.Parameters.AddWithValue("@productId", item.Product.Id);
                            itemCmd.Parameters.AddWithValue("@orderId", newOrderId);
                            itemCmd.Parameters.AddWithValue("@quantity", item.Quantity);

                            itemCmd.ExecuteNonQuery();
                        }
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

        public List<TopProductDto> GetTopSellingProducts(int topN = 3)
        {
            List<TopProductDto> result = new();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT TOP (@TopN)
                    P.ProductName,
                    SUM(C.Quantity) AS TotalSold
                FROM CartItems C
                JOIN Products P ON C.ProductId = P.ProductId
                GROUP BY P.ProductName
                ORDER BY TotalSold DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TopN", topN);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new TopProductDto
                                {
                                    ProductName = reader.GetString(0),
                                    TotalSold = reader.GetInt32(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting top-selling products: " + ex.Message);
            }

            return result;
        }

        public List<MonthlySalesDto> GetMonthlySalesReport()
        {
            var result = new List<MonthlySalesDto>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT 
                    FORMAT([Date], 'yyyy-MM') AS Month,
                    COUNT(*) AS OrderCount,
                    SUM(TotalPrice) AS TotalRevenue
                FROM Orders
                GROUP BY FORMAT([Date], 'yyyy-MM')
                ORDER BY Month DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new MonthlySalesDto
                            {
                                Month = reader.GetString(0),
                                OrderCount = reader.GetInt32(1),
                                TotalRevenue = reader.GetDecimal(2)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error generating monthly sales report: " + ex.Message);
            }

            return result;
        }

    }
}
