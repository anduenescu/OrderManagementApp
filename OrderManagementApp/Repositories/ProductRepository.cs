
using OrderManagementApp.Models;  

namespace OrderManagementApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> _product = new List<Product>();

        public ProductRepository()
        {
            var product = new Product();
            product.Id = 1;
            product.Name = "Test";
            product.Price = 100;
            product.Description = "Test Description";
            _product.Add(product);
        }
        public bool CreateProduct(Product product)
        {
            _product.Add(product);

            return true;
        }

        public bool DeleteProduct(int productId)
        {
            Product p = GetProduct(productId);
            if (p != null)
            {
                _product.Remove(p);
                return true;
            }
            return false;
        }

        public List<Product> GetAllProducts()
        {
            return _product;
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

        public bool UpdateProduct(Product updatedproduct)
        {
            Product p = GetProduct(updatedproduct.Id);
            if (p != null)
            {
                p.Name = updatedproduct.Name;
                p.Price = updatedproduct.Price;
                p.Description = updatedproduct.Description;
                return true;
            }
            return false;

        }
    }
}
