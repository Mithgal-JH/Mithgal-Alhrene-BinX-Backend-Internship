using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly Product[] _products = new Product[]
    {
        new Product(1, "Laptop", 999.99),
        new Product(2, "Gaming Mouse", 49.99),
        new Product(3, "Mechanical Keyboard", 89.99),
        new Product(4, "27-inch Monitor", 249.99),
        new Product(5, "USB-C Hub", 39.99),
        new Product(6, "External SSD 1TB", 129.99),
        new Product(7, "Wireless Headphones", 159.99),
        new Product(8, "Webcam", 69.99),
        new Product(9, "Smartphone", 799.99),
        new Product(10, "Tablet", 499.99)
    };
    [HttpGet]
    public IActionResult Get()
    {
        if (_products.Length == 0)
        {
            return NotFound();
        }
        return Ok(_products);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        foreach (var product in _products)
        {
            if (product.Id == id)
            {
                return Ok(product);
            }
        }

        return NotFound();
    }


}