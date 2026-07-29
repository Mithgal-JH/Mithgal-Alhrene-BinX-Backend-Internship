using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


Product[] _products = new Product[]
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

app.MapGet("/products", () =>
{
    if (_products.Length == 0)
    {
        return Results.NotFound();
    }
    return Results.Ok(_products);
}).WithName("GetProducts");

app.MapGet("/products/{id}", (int id) =>
{
    if (id <= 0)
    {
        return Results.BadRequest();
    }
    foreach (Product product in _products)
    {
        if (product.Id == id)
            return Results.Ok(product);
    }
    return Results.NotFound();
}).WithName("GetProductById");



app.MapControllers();

app.Run();
