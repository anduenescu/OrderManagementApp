using Microsoft.AspNetCore.Mvc;
using OrderManagementApp.Models;
using OrderManagementApp.Repositories;


namespace OrderManagementApp.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : Controller
    {
        ProductRepository ProductRepo;
        public ProductController(ProductRepository productRepo)
        {
            ProductRepo = productRepo;
        }

        public IActionResult Index()
        {
            return Ok("Hello World!");
        }



        [HttpGet("products")]

        public IActionResult Products()
        {
            return Ok(ProductRepo.GetAllProducts());
        }



        [HttpPost("addproduct")]
        public IActionResult AddProduct(Product newproduct)
        {

            return Ok(ProductRepo.CreateProduct(newproduct));
        }

        [HttpPut("editproduct")]
        public IActionResult EditProduct(Product editproduct)
        {

            return Ok(ProductRepo.UpdateProduct(editproduct));
        }

        [HttpDelete("deleteproduct")]
        public IActionResult DeleteProduct(int deleteproductId)
        {

            return Ok(ProductRepo.DeleteProduct(deleteproductId));
        }

        [HttpGet("getproduct")]
        public IActionResult GetProduct(int getproductId)
        {

            return Ok(ProductRepo.GetProduct(getproductId));
        }
        //add optional parameter that will get all products that start with Test
        [HttpGet("getproductsstartingwith")]
        public IActionResult GetProductsStartingWith(string startsWith)
        {
            return Ok(ProductRepo.GetProductsStartingWith(startsWith));
        }

        

    }
}
