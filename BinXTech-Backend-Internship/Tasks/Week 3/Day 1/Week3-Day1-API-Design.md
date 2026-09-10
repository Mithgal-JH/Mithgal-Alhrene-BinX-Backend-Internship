# Week 3 - Day 1
# Hands-On Lab: Design a REST Resource Map

This document contains the implementation of the Week 3 - Day 1 hands-on lab requirements.

---

# 1. Choose a small domain (e.g. a library catalog, a task tracker) and list its core resources as plural nouns.

## Domain

**Library Management System**

The system manages books available in a library and allows users to write reviews for each book.

### Core Resources

| Resource | Description |
|----------|-------------|
| Books | Represents all books available in the library. |
| Reviews | Represents reviews written for books. |

The resources are represented using **plural nouns** following REST API naming conventions.

---

# 2. Map out the full set of endpoints for one primary resource: list, get one, create, update, delete.

## Primary Resource

### Books

| HTTP Method | Endpoint | Description |
|-------------|----------|-------------|
| GET | `/api/v1/books` | Retrieve all books |
| GET | `/api/v1/books/{id}` | Retrieve a specific book |
| POST | `/api/v1/books` | Create a new book |
| PUT | `/api/v1/books/{id}` | Update an existing book |
| DELETE | `/api/v1/books/{id}` | Delete a book |

These endpoints provide the complete CRUD operations for the primary resource.

---

# 3. Add one nested resource endpoint reflecting a real ownership relationship.

## Nested Resource

Each **Book** can have multiple **Reviews**, while each **Review** belongs to exactly one **Book**.

Relationship:

```text
Book (1)
   │
   └──────────< Review (Many)
```

### Nested Endpoints

| HTTP Method | Endpoint | Description |
|-------------|----------|-------------|
| GET | `/api/v1/books/{id}/reviews` | Retrieve all reviews for a specific book |
| POST | `/api/v1/books/{id}/reviews` | Add a new review to a specific book |

The nested resource reflects a real ownership relationship because a Review cannot exist without a Book.

---

# 4. Write out the correct HTTP status code for each endpoint's success case and at least one error case.

| Endpoint | Success Case | Error Case |
|----------|--------------|------------|
| GET `/api/v1/books` | **200 OK** – Books retrieved successfully. | — |
| GET `/api/v1/books/{id}` | **200 OK** – Book found. | **404 Not Found** – Book does not exist. |
| POST `/api/v1/books` | **201 Created** – Book created successfully. | **400 Bad Request** – Invalid request body. |
| PUT `/api/v1/books/{id}` | **200 OK** – Book updated successfully. | **400 Bad Request** – Invalid request data.<br>**404 Not Found** – Book not found. |
| DELETE `/api/v1/books/{id}` | **204 No Content** – Book deleted successfully. | **404 Not Found** – Book not found. |
| GET `/api/v1/books/{id}/reviews` | **200 OK** – Reviews retrieved successfully. | **404 Not Found** – Book not found. |
| POST `/api/v1/books/{id}/reviews` | **201 Created** – Review created successfully. | **400 Bad Request** – Invalid request body.<br>**404 Not Found** – Book not found. |

The selected status codes follow standard REST API conventions.

---

# 5. Decide and document your API's versioning convention for this project.

## Versioning Strategy

This API uses **URL Versioning**.

### Base URL

```text
/api/v1
```

### Example

```http
GET /api/v1/books
```

### Why this strategy?

- Easy to understand.
- Easy to test using Postman.
- Commonly used in REST APIs.
- Allows future API changes without breaking existing API consumers.

---

# Conclusion

This document defines the REST Resource Map for a Library Management System before implementation.

The API follows REST conventions by:

- Modeling the API around resources.
- Using plural resource names.
- Defining complete CRUD endpoints.
- Using a nested resource for ownership.
- Returning appropriate HTTP status codes.
- Adopting URL versioning for future scalability.