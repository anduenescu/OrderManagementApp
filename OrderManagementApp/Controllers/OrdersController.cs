using Microsoft.AspNetCore.Mvc;
using OrderManagementApp.Exceptions;
using OrderManagementApp.Models;
using OrderManagementApp.Repositories;
using OrderManagementApp.Services;

namespace OrderManagementApp.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrdersController : Controller
    {
        OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("addorder")]
        public IActionResult AddOrder(Order neworder)
        {
            try
            {
                var result = _orderService.PlaceOrder(neworder);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create order.");
            }
        }

        [HttpPost("ordercart")]
        public IActionResult OrderCart(int userId)
        {
            try
            {
                var result = _orderService.OrderCart(userId);
                return Ok(result);
            }
            catch(CleanCartException ex)
            {
                return StatusCode(StatusCodes.Status206PartialContent, ex.Message);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create order.");
            }
        }
    }
}
