using Microsoft.Data.SqlClient;
using OrderManagementApp.Models;

namespace OrderManagementApp.Repositories
{
    public class CategoryRepository
    {
        private string _connectionString = "Data Source=DESKTOP-ALBJ45J\\SQLEXPRESS;Initial Catalog=OrderManagementPlatform;Integrated Security=True;Trust Server Certificate=True";

        public CategoryRepository() { }

        public Category? GetCategoryById(int Id)
        {
            try
            {
                Category? result = null;

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string queryCategory = "SELECT * FROM Categories WHERE CategoryId = @CategoryID";

                    using (SqlCommand command = new SqlCommand(queryCategory, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryID", Id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                result = new Category
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                    Name = reader.GetString(reader.GetOrdinal("CategoryName"))
                                };
                            }
                        }
                    }
                }

                return result;
            }
            catch (SqlException ex)
            {
                // Optionally log: Console.WriteLine("SQL Error: " + ex.Message);
                throw new Exception("A database error occurred while fetching the category.");
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occurred while fetching the category.");
            }
        }
    }
}
