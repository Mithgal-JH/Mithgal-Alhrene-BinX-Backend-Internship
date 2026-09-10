# Week 2 - Day 4: ASP.NET Core Web API

## 📌 Overview

On Day 4, I learned the fundamentals of building RESTful APIs using ASP.NET Core Web API. I explored both **Minimal APIs** and **Controllers**, understood the routing system, HTTP verbs, and tested endpoints using Postman.

---

## 📚 Topics Covered

- ASP.NET Core Web API
- Scaffolding a Web API project
- Minimal Hosting Model
- Minimal APIs
- Controllers
- Routing
- Route Parameters
- HTTP Verbs
- IActionResult
- ControllerBase
- API Testing with Postman

---

## 🛠️ Practical Tasks

### ✅ Minimal API

- Created GET endpoint to retrieve all products.
- Created GET endpoint to retrieve a product by ID.
- Tested endpoints successfully.

### ✅ Controllers

Created a `ProductsController` with:

- GET `/api/products`
- GET `/api/products/{id}`

Implemented proper HTTP responses:

- `200 OK`
- `400 Bad Request`
- `404 Not Found`

---

## 🧪 Testing

All endpoints were tested using **Postman**.

Included:

- GET All Products
- GET Product By ID

A Postman Collection has been exported and included in the project.

---

## 📂 Project Structure

```
HelloBinX-WebApi
│
├── Controllers
│   └── ProductsController.cs
├── Models
│   └── Product.cs
├── Postman
│   └── BinX-Training.postman_collection.json
├── Program.cs
└── README.md
```

---

## 📖 Resources Used

- BinX Backend .NET Internship Materials
- Microsoft Learn – ASP.NET Core Documentation
- Microsoft Learn – Web API Documentation
- ASP.NET Core Web API Tutorial (YouTube)
  https://www.youtube.com/watch?v=QSFWSjBHIkw
- ChatGPT (used for explanations, code review, and concept clarification)

---

## 🎯 Learning Outcome

By the end of this day, I can:

- Build a basic ASP.NET Core Web API.
- Understand the difference between Minimal APIs and Controllers.
- Create GET endpoints.
- Handle HTTP responses using IActionResult.
- Test APIs using Postman.
- Organize API requests into a Postman Collection.