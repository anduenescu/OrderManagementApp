using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementApp.Models;
using OrderManagementApp.Repositories;
using OrderManagementApp.Services;

namespace OrderManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ProductRepository ProductRepository;
        UserService UserService;
        public UserController(ProductRepository productRepository, UserService userService)
        {
            this.ProductRepository = productRepository;
            UserService = userService;
        }

        [Authorize]
        [HttpPost("addtocart")]
        public IActionResult AddToCart(int Id, int quantity)
        {
            var product = ProductRepository.GetProduct(Id);
            if (product.Stock >= quantity)
            {
                for (int i = 0; i < quantity; i++)
                {
                    AppUser.Cart.Add(product);

                }
                product.Stock = product.Stock - quantity;
                return Ok(AppUser.Cart);
            }
            else
            {
                return BadRequest("Not enough stock");
            }

        }

        [HttpPost("adduser")]
        public IActionResult AddUser(User user)
        {

            return Ok(UserService.CreateUser(user));
        }

    }
}
