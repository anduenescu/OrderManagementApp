using Microsoft.Data.SqlClient;
using OrderManagementApp.Models;

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

        public bool CreateProduct(Product product)
        {
            try
            {
                _product.Add(product);
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Products (ProductName, ProductDescription, Price, Stock, CategoryId) VALUES (@name, @description, @price, @stock, @CategoryID)";
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
            catch (Exception)
            {
                throw new Exception("An error occurred while creating the product.");
            }
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Products WHERE ProductId = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", productId);
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while deleting the product.");
            }
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                List<Product> result = new List<Product>();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Products";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product current = new Product
                                {
                                    Name = reader.GetString(reader.GetOrdinal("ProductName")),
                                    Description = reader.GetString(reader.GetOrdinal("ProductDescription")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                    Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                                    Id = reader.GetInt32(reader.GetOrdinal("ProductId"))
                                };
                                int categoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                                current.Category = CategoryRepo.GetCategoryById(categoryId);

                                result.Add(current);
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while retrieving all products.");
            }
        }

        public Product GetProduct(int productId)
        {
            try
            {
                Product result = null;
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Products WHERE ProductId = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", productId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                    Name = reader.GetString(reader.GetOrdinal("ProductName")),
                                    Description = reader.GetString(reader.GetOrdinal("ProductDescription")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                    Stock = reader.GetInt32(reader.GetOrdinal("Stock"))
                                };
                                int categoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                                product.Category = CategoryRepo.GetCategoryById(categoryId);

                                result = product;
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while retrieving the product.");
            }
        }

        public List<Product> GetProductsStartingWith(string startsWith)
        {
            try
            {
                List<Product> filteredProducts = new List<Product>();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Products WHERE ProductName LIKE @Prefix";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Prefix", startsWith + "%");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                    Name = reader.GetString(reader.GetOrdinal("ProductName")),
                                    Description = reader.GetString(reader.GetOrdinal("ProductDescription")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                    Stock = reader.GetInt32(reader.GetOrdinal("Stock"))
                                };
                                int categoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                                product.Category = CategoryRepo.GetCategoryById(categoryId);

                                filteredProducts.Add(product);
                            }
                        }
                    }
                }

                return filteredProducts;
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while searching products.");
            }
        }

        public bool UpdateProduct(Product updatedProduct)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Products SET ProductName = @name, ProductDescription = @description, Price = @price, Stock = @stock, CategoryId = @CategoryID WHERE ProductId = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", updatedProduct.Name);
                        command.Parameters.AddWithValue("@description", updatedProduct.Description);
                        command.Parameters.AddWithValue("@price", updatedProduct.Price);
                        command.Parameters.AddWithValue("@stock", updatedProduct.Stock);
                        command.Parameters.AddWithValue("@CategoryID", updatedProduct.Category.Id);
                        command.Parameters.AddWithValue("@Id", updatedProduct.Id);

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while updating the product.");
            }
        }
    }
}
