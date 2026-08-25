# Week 6 — Day 3
## Implementing Core Routes I: Catalog & Read Operations

### Project
Cardiac Patient Monitoring System

### Objective

Implement an efficient and scalable read endpoint for the Patients resource using:

- Pagination
- Filtering
- Sorting
- DTO Projection
- Efficient EF Core queries
- Avoiding over-fetching

---

## 1. Implemented Endpoint

### Get All Patients

```http
GET /api/Patients
```

The endpoint supports optional query parameters for pagination, filtering, and sorting.

### Supported Query Parameters

| Parameter | Description | Example |
|---|---|---|
| `page` | Page number | `page=1` |
| `pageSize` | Number of records per page | `pageSize=10` |
| `search` | Search by first or last name | `search=Ahmad` |
| `gender` | Filter by gender | `gender=Male` |
| `sort` | Sort by date of birth | `sort=dob_asc` / `sort=dob_desc` |

---

## 2. Pagination

The endpoint uses `Skip()` and `Take()` to return only the requested page instead of loading the entire Patients table.

Example:

```http
GET /api/Patients?page=1&pageSize=2
```

Response includes:

```json
{
  "items": [],
  "page": 1,
  "pageSize": 2,
  "totalCount": 8
}
```

`totalCount` is calculated after applying the filters and before pagination.

---

## 3. Filtering

Two optional filters were implemented.

### Search

Searches patient `FirstName` and `LastName`.

```http
GET /api/Patients?search=Ahmad
```

The filter is applied only when the `search` parameter is provided.

### Gender

Filters patients by gender.

```http
GET /api/Patients?gender=Male
```

The filter is also applied conditionally.

---

## 4. Sorting

Sorting is controlled through the `sort` query parameter.

### Ascending

```http
GET /api/Patients?sort=dob_asc
```

Sorts patients by `DateOfBirth` in ascending order.

### Descending

```http
GET /api/Patients?sort=dob_desc
```

Sorts patients by `DateOfBirth` in descending order.

The default behavior is ascending order by `DateOfBirth`.

---

## 5. DTO Projection

The endpoint does not return EF Core `Patient` entities directly.

Instead, the query projects the required fields into:

```text
PatientResponseDto
```

using LINQ `Select()`.

Example:

```csharp
.Select(p => new PatientResponseDto
{
    PatientId = p.PatientId,
    MedicalRecordNumber = p.MedicalRecordNumber,
    FirstName = p.FirstName,
    LastName = p.LastName,
    DateOfBirth = p.DateOfBirth,
    Gender = p.Gender,
    Phone = p.Phone,
    Email = p.Email,
    Address = p.Address,
    EmergencyContactName = p.EmergencyContactName,
    EmergencyContactPhone = p.EmergencyContactPhone,
    MedicalNotes = p.MedicalNotes
})
```

This prevents the API from exposing the EF Core entity directly and keeps the API response contract separate from the database entity.

---

## 6. Query Efficiency

The Patients read query uses:

```csharp
.AsNoTracking()
```

because the operation is read-only.

The query is built before execution and applies:

1. Filters
2. `CountAsync()`
3. Sorting
4. `Skip()`
5. `Take()`
6. DTO projection
7. `ToListAsync()`

This allows EF Core to translate the query into SQL and avoids loading unnecessary records into application memory.

---

## 7. Combined Query Example

Multiple parameters can be combined in a single request:

```http
GET /api/Patients?page=1&pageSize=2&search=Ahmad&gender=Male&sort=dob_desc
```

This demonstrates:

- Pagination
- Search filtering
- Gender filtering
- Descending sorting
- DTO projection

---

## 8. Postman Testing

The endpoint was tested successfully in Postman with the following scenarios:

### Pagination

```http
GET /api/Patients?page=1&pageSize=2
```

Result:

- `200 OK`
- 2 patients returned
- Correct page information
- Correct total count

### Search

```http
GET /api/Patients?search=Ahmad
```

Result:

- `200 OK`
- Only matching patients returned
- `totalCount` reflects the filtered result set

### Gender Filter + Sorting

```http
GET /api/Patients?gender=Male&sort=dob_desc
```

Result:

- `200 OK`
- Only male patients returned
- Results sorted by date of birth descending

### Combined Query

```http
GET /api/Patients?page=1&pageSize=2&search=Ahmad&gender=Male&sort=dob_desc
```

Result:

- `200 OK`
- Pagination, filtering, and sorting work together successfully

---

## 9. Validation

### Build

```text
dotnet build
```

Result:

```text
Build succeeded
```

### Tests

```text
dotnet test
```

Result:

```text
Build succeeded
```

### Application Run

The application started successfully using:

```text
dotnet run
```

Application URL:

```text
http://localhost:5180
```

---

## 10. Evidence

Postman screenshots were captured for:

1. Pagination
2. Search filtering
3. Gender filtering with sorting
4. Combined pagination, filtering, and sorting

---

## 11. Day 3 Completion

### Completed Requirements

- [x] Implement paginated GET endpoint
- [x] Add `page` and `pageSize`
- [x] Return `totalCount`
- [x] Add search filtering
- [x] Add gender filtering
- [x] Add ascending and descending sorting
- [x] Project entities to `PatientResponseDto`
- [x] Use `AsNoTracking()` for read-only queries
- [x] Avoid returning EF Core entities directly
- [x] Test pagination in Postman
- [x] Test filtering in Postman
- [x] Test sorting in Postman
- [x] Test combined query parameters
- [x] Verify successful build
- [x] Verify tests
- [x] Verify application startup

---

## Technologies Used

- ASP.NET Core
- C#
- Entity Framework Core
- LINQ
- PostgreSQL
- Postman
