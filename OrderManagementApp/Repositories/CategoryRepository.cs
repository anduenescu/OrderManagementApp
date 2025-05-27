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
            Category Result = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

             
                connection.Open();
          
                string queryCategory = "select * from Categories where categoryID = @CategoryID";
                using (SqlCommand command = new SqlCommand(queryCategory, connection))

                {
                    command.Parameters.AddWithValue("@CategoryID", Id);
             
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                          Result = new Category
                            {
                                Name = reader.GetString(reader.GetOrdinal("CategoryName")),                            
                                Id = reader.GetInt32(reader.GetOrdinal("CategoryId"))
                            };
                            

                            
                        }
                    }
                }
            }
            return Result;

        }
    }
}
