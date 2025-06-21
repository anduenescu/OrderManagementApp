using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementApp.Exceptions;
using OrderManagementApp.Models;
using OrderManagementApp.Services;

namespace OrderManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Applies to all actions in this controller
    public class AdminController : ControllerBase
    {
        private readonly UserService _userService; // added 06/07
        private readonly ProductService _productService; // added 06/07


        public AdminController(UserService userService, ProductService productService)
        {
            _userService = userService;
            _productService = productService;
        }

        [HttpGet("test")] // added 06/07
        public IActionResult Test()
        {
            return Ok("AdminController is working!");
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to retrieve users.");
            }
        }

        [HttpPost("product")] // added 06/07
        public IActionResult AddProduct([FromBody] Product product)
        {
            try
            {
                var result = _productService.AddProduct(product);
                if (result)
                    return Ok("Product added successfully!");
                return BadRequest("Failed to add product.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unexpected error while adding product.");
            }
        }

        [HttpPut("product")]
        public IActionResult EditProduct([FromBody] Product product)
        {
            try
            {
                var result = _productService.EditProduct(product);
                return result ? Ok("Product updated successfully!") : BadRequest("Failed to update product.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unexpected error while updating product.");
            }
        }

        [HttpDelete("product/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                bool result = _productService.DeleteProduct(id);
                return result ? Ok("Product deleted successfully!") : BadRequest("Failed to delete product.");
            }
            catch (IdFormatException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error while deleting product.");
            }
        }

        [HttpGet("products")]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _productService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to retrieve products.");
            }
        }
    }
}

