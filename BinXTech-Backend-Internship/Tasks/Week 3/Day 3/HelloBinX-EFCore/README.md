# Week 3 - Day 3
## Entity Framework Core & Code-First Migrations

### Overview
In this task, I learned the fundamentals of Entity Framework Core by creating a new ASP.NET Core Web API project and connecting it to SQL Server using the Code-First approach.

---

## What I Completed

- Created a new ASP.NET Core Web API project.
- Installed the required Entity Framework Core packages:
  - Microsoft.EntityFrameworkCore.SqlServer
  - Microsoft.EntityFrameworkCore.Tools
- Designed Entity classes based on the provided ERD:
  - Author
  - Book
  - Review
  - PhoneNumber
- Implemented Navigation Properties to represent relationships.
- Created `AppDbContext` and registered all `DbSet` properties.
- Configured the SQL Server connection string in `appsettings.json`.
- Registered `AppDbContext` in `Program.cs`.
- Generated the initial migration using:
  ```bash
  dotnet ef migrations add InitialCreate
  ```
- Applied the migration to SQL Server using:
  ```bash
  dotnet ef database update
  ```
- Successfully created the database and all tables from the entity models.

---

## Database Tables

- Authors
- Books
- Reviews
- PhoneNumbers
- __EFMigrationsHistory

---

## Concepts Learned

- Entity Framework Core
- Entity Classes
- DbContext
- DbSet
- Navigation Properties
- Primary Keys
- Foreign Keys
- Code-First Approach
- Migrations
- Connection Strings
- SQL Server Integration

---

## Project Structure

```
HelloBinX-EFCore
│
├── Controllers
├── Data
│   └── AppDbContext.cs
├── Models
│   ├── Author.cs
│   ├── Book.cs
│   ├── PhoneNumber.cs
│   └── Review.cs
├── Migrations
├── Program.cs
└── appsettings.json
```

---

## Configuration

The project uses SQL Server with a connection string configured in:

- `appsettings.json`

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=HelloBinXEFCoreDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---
## Result

The Entity Framework Core migration was successfully applied, and the SQL Server database was generated automatically based on the entity models and relationships defined in the project.