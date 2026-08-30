# BinX Tech — Backend Development Internship Tasks

This section contains my **weekly internship tasks**, practical exercises, and learning progress throughout the **Backend Development (.NET)** internship.

---

## 📊 Weekly Progress

| Week  | Focus                                        |    Status    | Documentation                       |
| :---: | -------------------------------------------- | :----------: | :---------------------------------- |
| **1** | C# & .NET Foundations                        | ✅ Completed | [View Week 1](./WEEK%201/README.MD) |
| **2** | Advanced C# & ASP.NET Core Foundations       | ✅ Completed | [View Week 2](./Week%202/README.md) |
| **3** | REST API, EF Core & CRUD                     | ✅ Completed | [View Week 3](./Week%203/README.md) |
| **4** | Authentication, Authorization & API Security | ✅ Completed | [View Week 4](./Week%204/README.md) |
| **5** | Testing, Error Handling & Project Begins  | ✅ Completed | [View Week 5](./Week%205/README.md) |
| **6** | Phase 3 Sprint 1 — Applied Project Work    | ✅ Completed | [View Week 6](./Week%206/README.md) |
| **7** | Phase 3 Sprint 2 — Authentication & Authorization | 🟡 In Progress | [View Week 7](./Week%207/README.md) |

---

## 🗓️ Weekly Overview

### Week 1 — C# & .NET Foundations

The first week focused on building the foundation required for backend development.

**Main topics:**

- .NET development environment
- C# fundamentals
- Object-Oriented Programming
- Collections
- LINQ
- `async/await`
- Git and GitHub workflow

📂 **Documentation:**  
[View Week 1](./WEEK%201/README.MD)

---

### Week 2 — Advanced C# & ASP.NET Core Foundations

The second week moved into more advanced C# concepts and the fundamentals of ASP.NET Core.

**Main topics:**

- Generics
- Advanced LINQ
- Deferred execution
- Asynchronous programming
- Concurrency basics
- ASP.NET Core Web API
- Routing
- Middleware
- Dependency Injection

📂 **Documentation:**  
[View Week 2](./Week%202/README.md)

---

### Week 3 — REST API, EF Core & CRUD

Week 3 focused on building a practical backend REST API and connecting it to a relational database.

**Main topics:**

- REST API design
- HTTP methods and status codes
- SQL Server
- Database schema design
- Normalization
- Entity Framework Core
- Code-First migrations
- Relationships
- CRUD operations
- Async database operations
- Postman testing
- API documentation

**Main project:** Book Management REST API

📂 **Documentation:**  
[View Week 3](./Week%203/README.md)

---

### Week 4 — Authentication, Authorization & API Security

Week 4 focused on securing the ASP.NET Core Web API through authentication, authorization, validation, and security hardening.

**Main topics:**

- ASP.NET Core Identity
- User registration
- Password hashing
- JWT Authentication
- JWT claims
- Protected API endpoints
- Role-Based Authorization
- Claims-Based Authorization
- Policy-Based Authorization
- FluentValidation
- CORS
- HTTPS Redirection
- HSTS
- Rate Limiting
- SQL Injection review

📂 **Documentation:**  
[View Week 4](./Week%204/README.md)

---

### Week 5 — Testing, Error Handling & Project Begins

Week 5 marked the transition from Phase 2 into Phase 3 and focused on testing, centralized error handling, and applying the learned concepts to the Cardiac Patient Monitoring System.

**Main topics:**

- xUnit unit testing
- Arrange–Act–Assert pattern
- Moq dependency mocking
- Repository pattern for testable services
- Integration testing with `WebApplicationFactory`
- Entity Framework Core In-Memory testing
- JWT authentication in integration tests
- Centralized exception handling
- `ProblemDetails`
- Structured logging with `ILogger`
- Full automated test suite

**Main project:** Cardiac Patient Monitoring System

**Final test result:** 15/15 tests passed

📂 **Documentation:**  
[View Week 5](./Week%205/README.md)

---

### Week 6 — Phase 3 Sprint 1 — Applied Project Work

Week 6 focused on strengthening the Cardiac Patient Monitoring System through applied backend development and production-oriented API design.

**Main topics:**

- Sprint Planning
- EF Core data modeling
- Fluent API configuration
- Entity relationships
- Delete behavior
- Unique indexes
- Seed data
- Code-First migrations
- PostgreSQL database verification
- Pagination, filtering, and sorting
- DTO projection
- Database transactions
- Rollback testing
- Postman verification
- Sprint Review and Retrospective

**Status:** ✅ Completed

📂 **Documentation:**  
[View Week 6](./Week%206/README.md)

---

### Week 7 — Phase 3 Sprint 2 — Authentication & Authorization

Week 7 focuses on implementing Authentication and Authorization in the Cardiac Patient Monitoring System.

**Main topics:**

- Sprint 2 Planning
- ASP.NET Core Identity integration
- Identity roles
- Role seeding
- Admin user seeding
- Authentication
- User Registration
- Login
- JWT-based authentication
- Role-Based Authorization (RBAC)
- Resource ownership
- Protected API endpoints

### Current Progress

**Day 1:** Sprint 2 Planning & Identity Integration — ✅ Completed

Day 1 included:

- Defined the Sprint 2 goal.
- Verified the existing `ApplicationDbContext` integration with `IdentityDbContext<IdentityUser>`.
- Verified the existing Identity migration.
- Defined the `Admin`, `Doctor`, and `Patient` roles.
- Verified role seeding through `IdentitySeeder`.
- Verified Admin account seeding.
- Defined initial authorization responsibilities.
- Identified resource ownership as an authorization requirement.

📂 **Documentation:**  
[View Week 7](./Week%207/README.md)

---

## 📁 Tasks Structure

```text
Tasks/
│
├── README.md
│
├── Week 1/
│   ├── README.md
│   ├── Day 1/
│   ├── Day 2/
│   ├── Day 3/
│   ├── Day 4/
│   └── Day 5/
│
├── Week 2/
│   ├── README.md
│   ├── Day 1/
│   ├── Day 2/
│   ├── Day 3/
│   ├── Day 4/
│   └── Day 5/
│
├── Week 3/
│   ├── README.md
│   ├── Day 1/
│   ├── Day 2/
│   ├── Day 3/
│   ├── Day 4/
│   └── Day 5/
│
├── Week 4/
│   ├── README.md
│   ├── Day 1/
│   ├── Day 2/
│   ├── Day 3/
│   ├── Day 4/
│   └── Day 5/
│
├── Week 5/
│   ├── README.md
│   ├── Day 1/
│   ├── Day 2/
│   ├── Day 3/
│   ├── Day 4/
│   └── Day 5/
│
└── Week 6/
    ├── README.md
    ├── Day 1/
    ├── Day 2/
    ├── Day 3/
    ├── Day 4/
    └── Day 5/
```

---

## 🚀 Internship Progress

**Weeks Completed:** 6 / 10  
**Current:** Week 7 — Day 1 Completed  
**Track:** Backend Development (.NET)
