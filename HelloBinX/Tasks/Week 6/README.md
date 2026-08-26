# Week 6 — Core API Development & Applied Backend Work

## Week Overview

Week 6 focuses on strengthening the Cardiac Patient Monitoring System through applied backend development and production-oriented API design.

**Status:** 🟢 In Progress  
**Current Day:** Day 4 — Completed  
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
| Day 4 | Database Transactions & Rollback Testing | ✅ Completed |
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

## Day 4 — Completed

### Database Transactions & Rollback Testing

Day 4 focused on implementing and testing a real multi-step write operation using a database transaction.

The selected operation was creating an Appointment together with Notifications for both the Patient and the Doctor.

Implemented:

- Added the `Notifications` entity and database table.
- Linked Notifications to Appointments.
- Updated Doctor creation so Admin-created Doctors are linked to an Identity User.
- Assigned the `Doctor` role to the created Identity User.
- Wrapped Doctor User + Doctor creation inside a transaction.
- Updated Appointment creation to use a database transaction.
- Created a Notification for the Patient.
- Created a Notification for the Doctor.
- Used `CommitAsync()` for successful operations.
- Used `RollbackAsync()` when an exception occurs.
- Tested the transaction using Postman and PostgreSQL.

### Transaction Flow

```text
Begin Transaction
        ↓
Create Appointment
        ↓
Create Patient Notification
        ↓
Create Doctor Notification
        ↓
Save Changes
        ↓
Commit Transaction
```

If an error occurs:

```text
Create Appointment
        ↓
Create Notifications
        ↓
Exception
        ↓
Rollback
        ↓
Appointment + Notifications are not persisted
```

### Rollback Test

A temporary exception was intentionally added before `CommitAsync()`:

```csharp
throw new Exception("Testing transaction rollback");
```

The request returned:

```text
500 Internal Server Error
```

The database was then checked and confirmed that the Appointment and related Notifications from the failed transaction were not persisted.

### Successful Test

After removing the temporary exception, the same request was executed again.

Result:

```text
201 Created
```

The Appointment and both Notifications were successfully stored in the database.

### Result

The transaction was successfully verified in both scenarios:

```text
Success → Commit
Failure → Rollback
```

The complete scenario and screenshots are documented in:

```text
Week_6_Day_4_Transaction_Rollback_Documentation_EN_Corrected.pdf
```

---

## Week 6 Progress

```text
Day 1  → ✅ Completed
Day 2  → ✅ Completed
Day 3  → ✅ Completed
Day 4  → ✅ Completed
Day 5  → ⏳ Pending
```

**Week 6 — In Progress 🟢**
