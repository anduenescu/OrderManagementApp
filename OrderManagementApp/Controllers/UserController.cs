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
        private readonly ProductRepository ProductRepository;
        private readonly UserService UserService;

        public UserController(ProductRepository productRepository, UserService userService)
        {
            ProductRepository = productRepository;
            UserService = userService;
        }

        [Authorize]
        [HttpPost("addtocart")]
        public IActionResult AddToCart(int Id, int quantity)
        {
            try
            {
                var product = ProductRepository.GetProduct(Id);

                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                if (product.Stock < quantity)
                {
                    return BadRequest("Not enough stock.");
                }

                for (int i = 0; i < quantity; i++)
                {
                    AppUser.Cart.Add(product);
                }

                product.Stock -= quantity;
                return Ok(AppUser.Cart);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add product to cart.");
            }
        }

        [HttpPost("adduser")]
        public IActionResult AddUser(User user)
        {
            try
            {
                var result = UserService.CreateUser(user);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create user.");
            }
        }
    }
}
