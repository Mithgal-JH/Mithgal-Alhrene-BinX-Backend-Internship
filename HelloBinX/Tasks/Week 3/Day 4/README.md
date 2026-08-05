# Week 3 - Day 4
## CRUD Operations with Entity Framework Core

### Overview
This project implements a complete RESTful CRUD API for the **Book** resource using ASP.NET Core Web API and Entity Framework Core with SQL Server.

The project follows REST conventions by using the appropriate HTTP methods, status codes, asynchronous database operations, and Dependency Injection.

---

## Features

- Create a new book
- Retrieve all books
- Retrieve a book by ID
- Update an existing book
- Delete a book
- Entity Framework Core Code-First Migrations
- SQL Server Integration
- Dependency Injection
- DTOs for Create and Update operations
- Seed Data for testing
- Postman Collection with success and error test cases

---

## Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- C#
- Dependency Injection
- Postman

---

## Project Structure

```
HelloBinX-CRUD_Operations
│
├── Controllers
├── Data
├── Dtos
├── Migrations
├── Models
├── Services
└── Program.cs
```

---

## API Endpoints

| Method | Endpoint | Description |
|---------|----------|-------------|
| GET | /api/books | Get all books |
| GET | /api/books/{id} | Get book by ID |
| POST | /api/books | Create a new book |
| PUT | /api/books/{id} | Update a book |
| DELETE | /api/books/{id} | Delete a book |

---

## HTTP Status Codes

| Endpoint | Success | Error |
|----------|---------|-------|
| GET All | 200 OK | — |
| GET By Id | 200 OK | 404 Not Found |
| Create | 201 Created | 400 Bad Request |
| Update | 200 OK | 404 Not Found / 400 Bad Request |
| Delete | 204 No Content | 404 Not Found |

---

## Database

The project uses SQL Server with Entity Framework Core Code-First.

Implemented relationships include:

- Author → Books (One-to-Many)
- Book → Reviews (One-to-Many)
- Author → PhoneNumbers (One-to-Many)

Seed data is included to simplify API testing.

---

## Testing

All CRUD endpoints were tested using Postman.

The collection includes:

- Success scenarios
- Error scenarios
- Invalid IDs
- Non-existing resources
- Foreign key validation test

---

## Documentation

Additional files included with this task:

- Postman Collection
- CRUD Test Results Documentation

---

## Learning Outcomes

During this task I practiced:

- REST API design
- Entity Framework Core CRUD operations
- Dependency Injection
- DTO usage
- SQL Server integration
- Code-First Migrations
- API testing using Postman
- Proper HTTP status codes