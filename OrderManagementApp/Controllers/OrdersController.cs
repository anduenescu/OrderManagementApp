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

            return Ok(OrderRepo.CreateOrder(neworder));
        }
    }


    

}
