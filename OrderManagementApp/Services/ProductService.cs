using OrderManagementApp.Exceptions;
using OrderManagementApp.Models;
using OrderManagementApp.Repositories;

namespace OrderManagementApp.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        // Admin-level operations
        public bool AddProduct(Product product)
        {
            try
            {
                return _productRepo.CreateProduct(product);
            }
            catch (Exception)
            {
                throw new Exception("Failed to add product.");
            }
        }

        public bool EditProduct(Product product)
        {
            try
            {
                return _productRepo.UpdateProduct(product);
            }
            catch (Exception)
            {
                throw new Exception("Failed to update product.");
            }
        }

        public bool DeleteProduct(int id)
        {
            if (id <= 0)
                throw new IdFormatException("The provided ID must be a positive integer greater than zero.");

            try
            {
                return _productRepo.DeleteProduct(id);
            }
            catch (Exception)
            {
                throw new Exception("Failed to delete product.");
            }
        }

        // Shared operations
        public List<Product> GetAllProducts()
        {
            try
            {
                return _productRepo.GetAllProducts();
            }
            catch (Exception)
            {
                throw new Exception("Failed to retrieve product list.");
            }
        }

        public Product GetProductById(int id)
        {
            if (id <= 0)
                throw new IdFormatException("The provided ID must be a positive integer greater than zero.");

            try
            {
                return _productRepo.GetProduct(id);
            }
            catch (Exception)
            {
                throw new Exception("Failed to retrieve product.");
            }
        }

        public List<Product> GetProductsStartingWith(string prefix)
        {
            try
            {
                return _productRepo.GetProductsStartingWith(prefix);
            }
            catch (Exception)
            {
                throw new Exception("Failed to search products by prefix.");
            }
        }
    }
}
