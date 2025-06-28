using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.DataClassification;
using OrderManagementApp.Models;
using OrderManagementApp.Repositories;
using OrderManagementApp.Services;

namespace OrderManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService UserService;

        public UserController(UserService userService)
        {
            UserService = userService;
        }

        [Authorize]
        [HttpPost("addtocart")]
        public IActionResult AddToCart(int userId, int IdProduct, int quantity)
        {
            try
            {
                UserService.AddToCart(userId, IdProduct,  quantity);
                return Ok();            }
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
