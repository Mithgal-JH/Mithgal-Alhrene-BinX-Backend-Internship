
using HelloBinX_Day5.Middleware;
using HelloBinX_Day5.Services;
using HelloBinX_Day5.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();


builder.Services.AddScoped<IProductService, ProductService>();
var app = builder.Build();




/*
    Tasks 1 & 2:

    1. Write a small custom middleware that logs each request's method and path to the console, and register it in Program.cs.

    2. Deliberately place it in the wrong pipeline order once, observe the effect, then correct the ordering.

    Notes:
    - The custom middleware must be registered before MapControllers().
    - If it is placed after MapControllers(), it won't execute because 
      the request is handled by the controller before reaching the middleware.
*/

app.UseMiddleware<RequestLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.MapControllers();

app.Run();
