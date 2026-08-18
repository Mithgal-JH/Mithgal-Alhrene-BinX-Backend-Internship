# Week 5 — Testing & Error Handling; Project Begins

## Week Overview

Week 5 marks the transition from Phase 2 into Phase 3.

The main focus of the week is **testing with xUnit and Moq, integration testing, centralized error handling, and beginning the applied project**.

**Status:** 🟡 In Progress  
**Current Day:** Day 3 — Completed  
**Week Status:** Not completed yet

## Week 5 Objectives

During this week, the main goals are to:

- Choose and scope the Phase 3 project.
- Write unit tests using xUnit.
- Use the Arrange–Act–Assert pattern.
- Learn to isolate dependencies with Moq.
- Write integration tests using WebApplicationFactory.
- Implement centralized exception handling with ProblemDetails.
- Apply structured logging for diagnosable failures.

## Daily Progress

| Day | Topic | Status |
|---|---|---|
| Day 1 | Choosing the Phase 3 Project & Unit Testing with xUnit | ✅ Completed |
| Day 2 | Mocking Dependencies with Moq | ✅ Completed |
| Day 3 | Integration Testing with WebApplicationFactory | ✅ Completed |
| Day 4 | Centralized Error Handling & Global Exception Middleware | ⏳ Not Started |
| Day 5 | Applying Testing to the Project & Week 5 Synthesis | ⏳ Not Started |

## Day 1 — Completed

### Project

The Phase 3 project direction was defined around:

**CardioCare — Cardiac Care & Monitoring Platform**

The existing Cardiac Patient Monitoring System from the previous phase is being used as the technical foundation and will be developed further during Phase 3.

### Scope

The project focuses on cardiac patient monitoring and follow-up, including:

- Patient profiles.
- Vital-sign monitoring.
- Medication tracking.
- Appointments.
- Health trends.
- Alerts.
- Authentication and authorization.

The project is intended to grow toward the professional backend baseline required by Week 9.

### xUnit

A separate test project was created:

```text
CardiacPatientMonitoringSystem.Tests
```

It references the main API project.

For Day 1, a pure business-logic method was added to `VitalSignService`:

```text
GetVitalSignStatus()
```

The test suite contains:

- 3 `[Fact]` tests.
- 1 `[Theory]` with 3 test cases.
- Arrange–Act–Assert structure.

### Test Result

```text
Total:   6
Passed:  6
Failed:  0
Skipped: 0
```

The tests were executed using:

```bash
dotnet test
```

## Evidence

Day 1 evidence includes:

- Project scope documentation.
- xUnit test project.
- Unit test code.
- Successful test run showing 6/6 tests passed.

---

## Day 2 — Completed

### Mocking Dependencies with Moq

Day 2 focused on testing a service that depends on a repository interface using **xUnit and Moq**.

The `PatientService` was refactored to depend on:

```text
IPatientRepository
```

instead of directly accessing:

```text
ApplicationDbContext
```

The resulting structure is:

```text
PatientService
      ↓
IPatientRepository
      ↓
PatientRepository
      ↓
ApplicationDbContext
      ↓
Database
```

During unit testing, the real repository is replaced with a Moq mock:

```text
PatientService
      ↓
Mock<IPatientRepository>
      ↓
Test Data
```

This isolates the service from the real database.

### Moq Features Practiced

The Day 2 tests demonstrated:

- `Mock<T>`
- `Setup()`
- `ReturnsAsync()`
- `ThrowsAsync()`
- `Verify()`
- `Times.Once`

### Test Scenarios

Tests were written for `PatientService.GetByIdAsync()` covering:

- Patient exists and the authenticated user is the owner.
- Patient does not exist.
- Patient belongs to another user.
- Repository throws an exception.

### Test Result

```text
Total:   10
Passed:  10
Failed:  0
Skipped: 0
```

The tests were executed successfully using the .NET test runner.

### Evidence

Day 2 evidence includes:

- `IPatientRepository`.
- `PatientRepository`.
- Updated `PatientService`.
- `PatientServiceTests` using Moq.
- Test result screenshot.
- Day 2 documentation.

### Commit

Day 2 was committed and pushed successfully to GitHub.

Commit:

```text
79481b8
Complete Week 5 Day 2 Moq repository testing
```

---

## Day 3 — Completed

### Integration Testing with WebApplicationFactory

Day 3 focused on integration testing the Cardiac Patient Monitoring System API using:

- xUnit
- Microsoft.AspNetCore.Mvc.Testing
- WebApplicationFactory
- Entity Framework Core In-Memory
- JWT Authentication

A custom test factory was created:

```text
CustomWebApplicationFactory
```

The factory configures a dedicated `Testing` environment and uses an **In-Memory database** instead of the development PostgreSQL database.

Test data was seeded for a Patient user:

```text
Email: patient17@example.com
Patient ID: 10
```

### Integration Test Scenarios

Tests were written for:

- Get patient by ID when the patient exists.
- Get patient by ID when the patient does not exist.
- Authentication using the real login endpoint.
- Attaching a valid JWT as a Bearer token.
- Verifying the protected patient endpoint.
- Verifying response status codes and response body data.

### Test Result

```text
Total:   2
Passed:  2
Failed:  0
Skipped: 0
```

The tests were executed successfully using:

```bash
dotnet test
```

### Test Environment

The integration tests use:

```text
Environment: Testing
Database: Entity Framework Core In-Memory
```

This keeps the integration tests isolated from the development PostgreSQL database.

### Evidence

Day 3 evidence includes:

- `PatientsApiIntegrationTests`.
- `CustomWebApplicationFactory`.
- Isolated In-Memory test database.
- Test-specific JWT configuration.
- Happy-path integration test.
- Not-found integration test.
- Successful test run showing 2/2 tests passed.

## Next

Day 4 will continue with **centralized error handling and global exception middleware**.

More sections will be added to this README as the remaining days of Week 5 are completed.
