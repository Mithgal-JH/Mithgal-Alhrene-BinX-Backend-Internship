
using HelloBinX_Day5.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HelloBinX_Day5.Services
{
    // Service responsible for providing product data
    public class ProductService : IProductService
    {
        // In-memory product collection (temporary data source)
        private readonly Product[] _products =  {
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

        // Retrieve all products
        public Product[] GetAllProducts()
        {
            return _products;
        }

        // Retrieve a product by its ID
        public Product? GetByID(int id)
        {
            foreach (var product in _products)
            {
                if (product.Id == id)
                {
                    return product;
                }
            }

            return null;
        }
    }
}