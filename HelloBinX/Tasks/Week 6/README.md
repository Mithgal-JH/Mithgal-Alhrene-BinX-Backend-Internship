# Week 6 — Core API Development & Applied Backend Work

## Week Overview

Week 6 focuses on strengthening the Cardiac Patient Monitoring System through applied backend development and production-oriented API design.

**Status:** 🟢 In Progress  
**Current Day:** Day 3 — Completed  
**Week Status:** In Progress

## Week Objectives

During this week, the main goals are to:

- Strengthen the Cardiac Patient Monitoring System.
- Apply Entity Framework Core patterns.
- Improve API design and read operations.
- Implement production-style API endpoints.
- Practice database configuration and seeding.
- Implement pagination, filtering, and sorting.
- Use DTO projection and efficient queries.
- Validate the implementation through testing and Postman.

## Daily Progress

| Day | Topic | Status |
|---|---|---|
| Day 1 | Sprint Planning & Project Organization | ✅ Completed |
| Day 2 | EF Core Data Model, Fluent API & Database Seeding | ✅ Completed |
| Day 3 | Core Routes I: Catalog & Read Operations | ✅ Completed |
| Day 4 | — | ⏳ Pending |
| Day 5 | — | ⏳ Pending |

---

## Day 1 — Completed

### Sprint Planning & Project Organization

Day 1 focused on planning the sprint and organizing the Cardiac Patient Monitoring System for the next development phase.

Key work included:

- Sprint planning.
- Defining development tasks.
- Organizing the project structure.
- Preparing the project for continued backend development.

---

## Day 2 — Completed

### EF Core Data Model, Fluent API & Database Seeding

Day 2 focused on strengthening the EF Core data layer.

Key work included:

- Reviewing the existing EF Core entities.
- Configuring relationships using Fluent API.
- Defining `DeleteBehavior.Restrict`.
- Configuring indexes and constraints.
- Adding medication seed data using `HasData`.
- Creating and reviewing an EF Core migration.
- Applying the migration to PostgreSQL.
- Verifying the database state.
- Running the automated test suite.

### Result

```text
15 Tests
15 Passed
0 Failed
```

---

## Day 3 — Completed

### Core Routes I: Catalog & Read Operations

Day 3 focused on improving the Patients read endpoint with production-style query capabilities.

Implemented:

- Pagination using `page` and `pageSize`.
- Search filtering by first and last name.
- Gender filtering.
- Sorting by `DateOfBirth`.
- `totalCount` after filtering.
- `PatientResponseDto` projection using LINQ `Select()`.
- `AsNoTracking()` for read-only queries.
- Combined pagination, filtering, and sorting.

### Example

```text
GET /api/Patients?page=1&pageSize=2&search=Ahmad&gender=Male&sort=dob_desc
```

All Postman scenarios returned:

```text
200 OK
```

### Validation

```text
dotnet build  → Build succeeded
dotnet test   → Successful
dotnet run    → Application started successfully
```

### Evidence

Day 3 evidence includes:

- Pagination screenshot.
- Search screenshot.
- Gender + sorting screenshot.
- Combined query screenshot.

---

## Week 6 Progress

```text
Day 1  → ✅ Completed
Day 2  → ✅ Completed
Day 3  → ✅ Completed
Day 4  → ⏳ Pending
Day 5  → ⏳ Pending
```

**Week 6 — In Progress 🟢**
