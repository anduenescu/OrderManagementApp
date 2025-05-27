
using Microsoft.Data.SqlClient;
using OrderManagementApp.Models;
using System.Data.SqlClient;

namespace OrderManagementApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> _product = new List<Product>();
        private string _connectionString = "Data Source=DESKTOP-ALBJ45J\\SQLEXPRESS;Initial Catalog=OrderManagementPlatform;Integrated Security=True;Trust Server Certificate=True";
        private CategoryRepository CategoryRepo;
        
        public ProductRepository(CategoryRepository categoryRepo)
        {
            CategoryRepo = categoryRepo;
        }

     
        public bool CreateProduct(Product product) //added to database 
        {
            _product.Add(product);
            using (SqlConnection connection = new SqlConnection(_connectionString)) 
            {
                connection.Open();
                string query = "insert into Products (ProductName, ProductDescription, Price, Stock,CategoryId) values (@name, @description, @price, @Stock, @CategoryID)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", product.Name);
                    command.Parameters.AddWithValue("@description", product.Description);
                    command.Parameters.AddWithValue("@price", product.Price);
                    command.Parameters.AddWithValue("@stock", product.Stock);
                    command.Parameters.AddWithValue("@CategoryID", product.Category.Id);

                    command.ExecuteNonQuery();
                }
            }
            return true;
        }

        public bool DeleteProduct(int productId) // !!! add to database 
        {

            
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "delete Products WHERE ProductID = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
        
                    command.Parameters.AddWithValue("@Id", productId);

                    command.ExecuteNonQuery();
                    return true;
                }

            }

        }

        public List<Product> GetAllProducts()
        {
            List<Product> result = new List<Product>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                int categoryId = 0; 
                connection.Open();
                string query = "select * from Products";
                string queryCategory = "select * from Categories";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product Current = new Product
                            {
                                Name = reader.GetString(reader.GetOrdinal("ProductName")),
                                Description = reader.GetString(reader.GetOrdinal("ProductDescription")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                                Id = reader.GetInt32(reader.GetOrdinal("ProductId"))
                            };
                            categoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                            Current.Category = CategoryRepo.GetCategoryById(categoryId);

                            result.Add(Current);
                            

                        }
                    }
                }
            }
                    return result;

        }

        
        public Product GetProduct(int productId)
        {
            foreach (Product product in _product)
            {
                if (product.Id == productId)
                {
                    return product;
                }
            }
            return null;
        }
        public List<Product> GetProductsStartingWith(string startsWith)
        {
            List<Product> filteredProducts = new List<Product>();

            foreach (Product product in GetAllProducts())
            {
                if (product.Name != null && product.Name.StartsWith(startsWith, StringComparison.OrdinalIgnoreCase))
                {
                    filteredProducts.Add(product);
                }
            } 

            return filteredProducts;          
        }

                     
        public bool UpdateProduct(Product updatedproduct)
        {
            //Product p = GetProduct(updatedproduct.Id);
            // TO do check if the product exists 
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "update Products set ProductName = @name, ProductDescription = @description, Price=@price, Stock=@Stock, CategoryId=@CategoryID WHERE ProductID = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", updatedproduct.Name);
                    command.Parameters.AddWithValue("@description", updatedproduct.Description);
                    command.Parameters.AddWithValue("@price", updatedproduct.Price);
                    command.Parameters.AddWithValue("@stock", updatedproduct.Stock);
                    command.Parameters.AddWithValue("@CategoryID", updatedproduct.Category.Id);
                    command.Parameters.AddWithValue("@Id", updatedproduct.Id);

                    command.ExecuteNonQuery();
                    return true;
                }

            }


        }
    }
}
