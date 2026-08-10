using CardiacPatientMonitoringSystem.Data;
using Microsoft.EntityFrameworkCore;
using CardiacPatientMonitoringSystem.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Registers application services for dependency injection.
builder.Services.AddApplicationServices();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();