using OrderManagementApp.Models;

namespace OrderManagementApp.Repositories
{
    public interface IProductRepository
    {
        public List<Product> GetAllProducts();
        public List<Product> GetProductsStartingWith(string startsWith);  
        public bool CreateProduct(Product product);
        public bool UpdateProduct(Product product);
        public bool DeleteProduct(int productId);
        public Product GetProduct(int productId);
        
    }
}
