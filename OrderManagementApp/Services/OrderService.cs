using OrderManagementApp.Models;
using OrderManagementApp.Repositories;
using OrderManagementApp.Models.DTOs;
using System.ComponentModel;
using OrderManagementApp.Exceptions;

namespace OrderManagementApp.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepo;
        private readonly ProductService _productService;

        public OrderService(OrderRepository orderRepo, ProductService productService)
        {
            _orderRepo = orderRepo;
            _productService = productService;
        }

        public bool PlaceOrder(Order order)
        {
            try
            {
                // TODO: Add logic for total price calc, stock check, etc.
                
                return _orderRepo.CreateOrder(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error placing order: " + ex.Message);
                return false;
            }
        }

        public List<MonthlySalesDto> GetMonthlySalesReport()
        {
            try
            {
                return _orderRepo.GetMonthlySalesReport();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating monthly sales report: " + ex.Message);
                return new List<MonthlySalesDto>(); // return empty list as fallback
            }
        }

        public List<TopProductDto> GetTopSellingProducts()
        {
            try
            {
                return _orderRepo.GetTopSellingProducts();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving top-selling products: " + ex.Message);
                return new List<TopProductDto>();
            }
        }

        public bool OrderCart(int userId)
        {
            //get the current user cart
            List<OrderItem> cartItems = _orderRepo.GetUserCart(userId);
            decimal price = 0;
            foreach (OrderItem item in cartItems) {
                if(!_productService.IsInStock(item.Product.Id, item.Quantity))
                    return false;

                decimal priceLine = item.Quantity * item.Product.Price;
                price += priceLine;
            }
            //create an order
            Order order = new Order { Date = DateTime.Now, Status = "paid", UserId = userId, Items = cartItems, TotalPrice = decimal.ToDouble(price)};

            //pass the order
            bool resultOrder = _orderRepo.CreateOrder(order);

            if (!resultOrder)
                throw new Exception("Order not passed");
            
            //Todo remove quantity
            foreach (OrderItem item in cartItems) {
                //Remove item.quantity of item.product    
             }

            //clean the cart
            bool resultCleanCart = _orderRepo.CleanCart(userId);

            if (!resultCleanCart)
                throw new CleanCartException("Order done but cart was not cleared");

            return resultOrder;
        }
    }
}