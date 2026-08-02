# Week 3 - Day 1
## REST API Design Principles & Resource Modeling

### Overview

Today focused on learning the fundamentals of designing RESTful APIs before writing any implementation code. Instead of building an API, the goal was to design and document a clean REST resource map following industry best practices.

---

## Topics Covered

- REST API Design Principles
- Resource Modeling
- REST Resource Naming Conventions
- HTTP Methods
- HTTP Status Codes
- Nested Resources
- API Versioning

---

## Key Concepts Learned

### RESTful Resource Design

- APIs should be designed around **resources (nouns)** rather than actions (verbs).
- Resource names should use **plural nouns**.
- HTTP methods define the action instead of the endpoint name.

Example:

```http
GET    /api/v1/books
POST   /api/v1/books
PUT    /api/v1/books/{id}
DELETE /api/v1/books/{id}
```

---

### Resource Relationships

Designed a nested resource to represent ownership.

Example:

```http
GET /api/v1/books/{id}/reviews
```

A Book can have multiple Reviews.

---

### HTTP Status Codes

Used proper HTTP status codes according to REST conventions.

| Status Code | Usage |
|------------|----------------------------|
| 200 OK | Successful retrieval/update |
| 201 Created | Resource created successfully |
| 204 No Content | Resource deleted successfully |
| 400 Bad Request | Invalid request data |
| 401 Unauthorized | Authentication required |
| 403 Forbidden | Permission denied |
| 404 Not Found | Resource not found |

---

### API Versioning

Selected **URL Versioning** as the API versioning strategy.

Example:

```http
/api/v1/books
```

Reasons:

- Easy to understand
- Easy to test using Postman
- Widely used in REST APIs
- Supports future API evolution without breaking existing clients

---

## Hands-On Lab

Designed a REST Resource Map for a **Library Management System**.

### Resources

- Books
- Reviews

### Primary Resource

- Get all books
- Get book by ID
- Create book
- Update book
- Delete book

### Nested Resource

- Get reviews for a specific book
- Add a review to a specific book

Each endpoint was documented with the appropriate success and error HTTP status codes.

---

## Project Files

```
Week 3/
└── Day 1/
    ├── API-Design.md
    └── README.md
```

---

## Skills Practiced

- RESTful API Design
- Resource Modeling
- Endpoint Planning
- HTTP Status Code Selection
- Nested Resource Design
- API Versioning Strategy
- Technical Documentation

---

## References

- BinX Backend .NET Internship – Week 3 Resources
- REST API Design Principles (BinX Resource)
- W3Schools – HTTP Methods
- W3Schools – HTTP Status Codes
- W3Schools – REST API Tutorial

---

## Notes

This day focused entirely on **API design and documentation** rather than implementation. Establishing a well-designed API contract before development helps ensure consistency, maintainability, and scalability throughout the project.