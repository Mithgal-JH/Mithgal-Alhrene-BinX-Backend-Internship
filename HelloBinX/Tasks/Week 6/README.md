# Week 6 — Core API Development & Applied Backend Work

## Week Overview

Week 6 focuses on strengthening the Cardiac Patient Monitoring System through applied backend development and progressively more production-oriented API design.

**Status:** 🟢 In Progress  
**Current Day:** Day 3 — Completed  
**Week Status:** In Progress

## Week 6 Objectives

During this week, the main goals are to:

- Strengthen the Cardiac Patient Monitoring System.
- Continue applying Entity Framework Core patterns.
- Implement production-style read operations.
- Build efficient paginated API endpoints.
- Support filtering and sorting through query parameters.
- Project entities to DTOs.
- Validate the implemented endpoints through Postman and automated checks.

## Daily Progress

| Day | Topic | Status |
|---|---|---|
| Day 1 | Sprint Planning & Project Organization | ✅ Completed |
| Day 2 | EF Core Data Model, Fluent API & Database Seeding | ✅ Completed |
| Day 3 | Implementing Core Routes I: Catalog & Read Operations | ✅ Completed |

---

## Day 3 — Completed

### Implementing Core Routes I: Catalog & Read Operations

Day 3 focused on implementing efficient and scalable read operations for the Patients resource in the Cardiac Patient Monitoring System.

The main work included:

- Pagination using `page` and `pageSize`.
- Conditional filtering using `search` and `gender`.
- Sorting using the `sort` query parameter.
- DTO projection using `PatientResponseDto`.
- Efficient read-only queries with `AsNoTracking()`.
- Returning `totalCount` after filtering.
- Testing all scenarios through Postman.

### Patients List Endpoint

The main endpoint was updated:

```text
GET /api/Patients
```

Supported query parameters:

```text
page
pageSize
search
gender
sort
```

Example:

```text
GET /api/Patients?page=1&pageSize=2&search=Ahmad&gender=Male&sort=dob_desc
```

### Pagination

Pagination was implemented using:

```csharp
.Skip((page - 1) * pageSize)
.Take(pageSize)
```

Example:

```text
GET /api/Patients?page=1&pageSize=2
```

The response includes:

```json
{
  "items": [],
  "page": 1,
  "pageSize": 2,
  "totalCount": 8
}
```

### Filtering

Two optional filters were implemented.

#### Search

Searches both `FirstName` and `LastName`:

```text
GET /api/Patients?search=Ahmad
```

#### Gender

Filters patients by gender:

```text
GET /api/Patients?gender=Male
```

### Sorting

Sorting is controlled through the `sort` parameter.

Ascending:

```text
GET /api/Patients?sort=dob_asc
```

Descending:

```text
GET /api/Patients?sort=dob_desc
```

Implementation:

```csharp
if (sort == "dob_desc")
    query = query.OrderByDescending(p => p.DateOfBirth);
else
    query = query.OrderBy(p => p.DateOfBirth);
```

### DTO Projection

The endpoint does not return EF Core `Patient` entities directly.

The query projects the selected fields into:

```text
PatientResponseDto
```

using LINQ `Select()`.

The final response type is:

```text
PaginatedResponseDto<PatientResponseDto>
```

### Query Flow

```text
AsNoTracking()
      ↓
Filters
      ↓
CountAsync()
      ↓
Sorting
      ↓
Skip()
      ↓
Take()
      ↓
Select() → PatientResponseDto
      ↓
ToListAsync()
```

### Postman Testing

The endpoint was tested successfully with:

- Pagination.
- Search filtering.
- Gender filtering.
- Ascending sorting.
- Descending sorting.
- Combined pagination, filtering, and sorting.

All tested scenarios returned:

```text
200 OK
```

### Build & Test

The project was verified using:

```powershell
dotnet build
dotnet test
dotnet run
```

Build result:

```text
Build succeeded
```

The test command completed successfully with no test failures reported.

The application started successfully on:

```text
http://localhost:5180
```

### Build Warning

The build reported:

```text
NU1903
Package 'Microsoft.OpenApi' 2.0.0 has a known high severity vulnerability
```

This warning did not cause the build or test command to fail.

### Evidence

Day 3 evidence includes:

- Pagination test screenshot.
- Search filter test screenshot.
- Gender + sorting test screenshot.
- Combined query test screenshot.
- Successful build and test execution.

### Day 3 Outcome

Day 3 successfully implemented the core read operations for the Patients resource with:

- Pagination.
- Filtering.
- Sorting.
- DTO projection.
- Efficient EF Core querying.
- Postman verification.

**Day 3 — Completed ✅**
