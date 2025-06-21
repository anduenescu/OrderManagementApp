using Microsoft.AspNetCore.Mvc;
using OrderManagementApp.Models;
using OrderManagementApp.Repositories;

namespace OrderManagementApp.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrdersController : Controller
    {
        OrderRepository OrderRepo;

        public OrdersController(OrderRepository orderRepo)
        {
            OrderRepo = orderRepo;
        }

        [HttpPost("addorder")]
        public IActionResult AddOrder(Order neworder)
        {
            try
            {
                var result = OrderRepo.CreateOrder(neworder);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create order.");
            }
        }
    }
}
