using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementApp.Models;
using OrderManagementApp.Repositories;

namespace OrderManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ProductRepository ProductRepository;
        public UserController(ProductRepository productRepository)
        {
            this.ProductRepository = productRepository;
        }

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
        
    }
}
