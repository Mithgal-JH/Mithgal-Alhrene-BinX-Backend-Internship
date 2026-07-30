# Week 2 – ASP.NET Core & Modern C# Development

## Overview

During Week 2 of the BinX Backend .NET Internship, I transitioned from core C# concepts to building real backend applications with ASP.NET Core Web API.

This week focused on writing cleaner and more maintainable code using generics, LINQ, asynchronous programming, middleware, dependency injection, and service-based architecture. I also built my first ASP.NET Core Web API and tested it using Postman.

---

## Learning Objectives

- Understand Generic Collections and Generic Classes.
- Build reusable code using Generics.
- Practice querying data with LINQ.
- Learn asynchronous programming using async and await.
- Build RESTful APIs using ASP.NET Core.
- Understand the ASP.NET Core request pipeline.
- Implement custom Middleware.
- Learn Dependency Injection and Service Registration.
- Separate business logic from controllers using the Service Layer.

---

# Day 1 – Generics & Collections

## Topics Covered

- Generic Collections
- Generic Classes
- Generic Methods
- Type Safety
- Reusable Components

## Tasks Completed

- Created generic classes.
- Implemented generic methods.
- Worked with `List<T>`.
- Practiced reusable C# code.

---

# Day 2 – Generic Repository

## Topics Covered

- Repository Pattern
- Generic Repository
- CRUD Operations
- Code Reusability

## Tasks Completed

- Built a generic repository.
- Implemented Create, Read, Update and Delete operations.
- Improved code organization using generics.

---

# Day 3 – LINQ & Asynchronous Programming

## Topics Covered

### LINQ

- Where()
- Select()
- OrderBy()
- OrderByDescending()
- FirstOrDefault()
- Any()
- Count()

### Async Programming

- async
- await
- Task
- Task<T>

## Tasks Completed

- Queried collections using LINQ.
- Built asynchronous methods.
- Learned the difference between synchronous and asynchronous execution.
- Improved application responsiveness using async programming.

---

# Day 4 – ASP.NET Core Web API

## Topics Covered

- ASP.NET Core
- Web API
- Controllers
- Routing
- Action Results
- HTTP Verbs
- REST API Principles

## Tasks Completed

- Created an ASP.NET Core Web API project.
- Built REST endpoints.
- Implemented GET endpoints.
- Returned appropriate HTTP status codes.
- Tested endpoints using Postman.

---

# Day 5 – Middleware & Dependency Injection

## Topics Covered

### Middleware

- Request Pipeline
- Custom Middleware
- Middleware Ordering

### Dependency Injection

- Interfaces
- Services
- Constructor Injection
- Service Registration
- Service Lifetimes

## Tasks Completed

- Created a custom request logging middleware.
- Registered middleware inside the request pipeline.
- Tested middleware ordering.
- Created `IProductService`.
- Implemented `ProductService`.
- Registered the service using `AddScoped`.
- Injected the service into `ProductsController`.
- Moved business logic from the controller to the service layer.
- Tested all API endpoints using Postman.

---

# Technologies Used

- C#
- .NET 9
- ASP.NET Core Web API
- Visual Studio Code
- Postman
- Git
- GitHub
- Notion

---

# Resources Used

### Official Documentation

- Microsoft Learn
- Microsoft ASP.NET Core Documentation

### Video Tutorials

- Week 2 learning resources provided by BinX
- Additional YouTube tutorials when needed

### Additional Learning

- GeeksforGeeks – Dependency Injection Design Pattern
- ChatGPT (concept explanations, debugging, best practices, and code review)

---

# Key Learning Outcomes

By the end of Week 2, I was able to:

- Build reusable C# components using Generics.
- Query collections efficiently using LINQ.
- Write asynchronous methods using async/await.
- Develop RESTful APIs with ASP.NET Core.
- Understand the ASP.NET Core request pipeline.
- Implement custom middleware.
- Apply Dependency Injection using interfaces and services.
- Separate business logic from controllers using the Service Layer.
- Test APIs professionally using Postman.

---

# Repository Structure

```text
Week 2
│
├── Day 1
│   └── Generics & Collections
│
├── Day 2
│   └── Generic Repository
│
├── Day 3
│   └── LINQ & Async Programming
│
├── Day 4
│   └── ASP.NET Core Web API
│
└── Day 5
    └── Middleware & Dependency Injection
```

---

# Final Notes

Week 2 marked my transition from learning advanced C# language features to building real backend applications with ASP.NET Core. It introduced key software engineering concepts such as middleware, dependency injection, and layered architecture, which are fundamental for developing scalable and maintainable backend systems.