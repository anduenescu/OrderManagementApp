using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementApp.Services;
using OrderManagementApp.Models;
using OrderManagementApp.Exceptions;

[Route("api/products")]
[ApiController]
[Authorize] // All endpoints require authentication unless marked with [AllowAnonymous]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        try
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to retrieve products.");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        try
        {
            var product = _productService.GetProductById(id);
            return product != null ? Ok(product) : NotFound("Product not found.");
        }
        catch (IdFormatException ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error retrieving product.");
        }
    }

    [HttpGet("search")]
    public IActionResult GetProductsStartingWith([FromQuery] string prefix)
    {
        try
        {
            if (string.IsNullOrEmpty(prefix))
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, "Prefix cannot be empty.");
            }

            var result = _productService.GetProductsStartingWith(prefix);
            return Ok(result ?? new List<Product>());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to search products.");
        }
    }

    [HttpGet("status")]
    [AllowAnonymous]
    public IActionResult Index()
    {
        return Ok("Product API is alive");
    }
}
