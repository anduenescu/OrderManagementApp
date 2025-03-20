using OrderManagementApp.Models;

namespace OrderManagementApp.Repositories
{
    public interface IProductRepository
    {
        public List<Product> GetAllProducts();
        public Product GetProduct(int productId);

        public bool CreateProduct(Product product);
        public bool UpdateProduct(Product product);
        public bool DeleteProduct(int productId);

    }
}
