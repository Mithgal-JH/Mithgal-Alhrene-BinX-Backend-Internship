namespace HelloBinX_Day5.Services.Interfaces
{
    // Defines the contract for product-related operations
    public interface IProductService
    {
        // Returns all products
        Product[] GetAllProducts();
        
        // Returns a single product by ID
        Product? GetByID(int id);
    }
}