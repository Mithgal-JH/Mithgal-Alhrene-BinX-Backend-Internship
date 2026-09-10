# Week 2 - Day 5: Middleware & Dependency Injection

## Overview

On Day 5, I learned how the ASP.NET Core request pipeline works through Middleware and how Dependency Injection (DI) helps build loosely coupled and maintainable applications. I also refactored the API by moving business logic from the controller into a dedicated service layer.

---

## Topics Covered

- Middleware in ASP.NET Core
- Request Processing Pipeline
- Custom Middleware
- Middleware Ordering
- Dependency Injection (DI)
- Service Registration
- Constructor Injection
- Service Layer Architecture

---

## Tasks Completed

### 1. Custom Request Logging Middleware

Created a custom middleware that logs every incoming HTTP request by printing the request method and request path to the console.

Example output:

```text
Request: GET /api/products
Request: GET /api/products/5
```

---

### 2. Middleware Pipeline

- Registered the custom middleware in `Program.cs`.
- Tested middleware ordering.
- Observed how middleware execution depends on its position in the request pipeline.

---

### 3. Dependency Injection

Implemented Dependency Injection using the built-in ASP.NET Core DI container.

Created:

- `IProductService`
- `ProductService`

Registered the service:

```csharp
builder.Services.AddScoped<IProductService, ProductService>();
```

---

### 4. Service Layer

Moved all product-related business logic from the controller into `ProductService`.

The service is responsible for:

- Retrieving all products
- Retrieving a product by ID

The controller is responsible only for:

- Receiving HTTP requests
- Returning HTTP responses
- Calling the service

---

### 5. Constructor Injection

Injected the service into `ProductsController` using constructor injection.

```csharp
public ProductsController(IProductService productService)
{
    _productService = productService;
}
```

---

### 6. API Testing

Tested the API using the browser and Postman.

Verified:

- Get all products
- Get product by ID
- Invalid ID returns BadRequest
- Unknown ID returns NotFound
- Middleware logs every request

---

## Project Structure

```
HelloBinX-Day5
│
├── Controllers
│   └── ProductsController.cs
│
├── Middleware
│   └── RequestLoggingMiddleware.cs
│
├── Models
│   └── Product.cs
│
├── Services
│   ├── Interfaces
│   │   └── IProductService.cs
│   └── ProductService.cs
│
└── Program.cs
```

---

## Key Takeaways

- Understood how Middleware processes every HTTP request.
- Learned why middleware ordering is important.
- Learned how ASP.NET Core Dependency Injection works.
- Understood the role of interfaces in reducing coupling.
- Practiced constructor injection.
- Improved project structure by separating business logic from controllers.

---

## Resources Used

- BinX Internship Week 2 Materials
- Microsoft Learn Documentation
- Official ASP.NET Core Documentation
- GeeksforGeeks – Dependency Injection Design Pattern
- ChatGPT (concept explanations, code review, debugging, and best practices)