using HelloBinX_Day5.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    
    // Inject the product service using Dependency Injection
    private readonly IProductService _productService; 

    public ProductsController(IProductService productService)
    {
        _productService=productService;
    }


    // Returns all available products
    [HttpGet]
    public IActionResult Get()
    {
        var products= _productService.GetAllProducts();

        if (products.Length == 0)
        {
            return NotFound();
        }
        return Ok(products);
    }


    // Returns a single product by its ID
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (id <=0)
        {
            return BadRequest();
        }
        var product = _productService.GetByID(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }


}