# Week 5 — Testing & Error Handling; Project Begins

## Week Overview

Week 5 marks the transition from Phase 2 into Phase 3.

The main focus of the week is **testing with xUnit and Moq, integration testing, centralized error handling, and beginning the applied project**.

**Status:** 🟢 Completed  
**Current Day:** Day 5 — Completed  
**Week Status:** Completed

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
| Day 4 | Centralized Error Handling & Global Exception Middleware | ✅ Completed |
| Day 5 | Applying Testing to the Project & Week 5 Synthesis | ✅ Completed |

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

## Day 4 — Completed

### Centralized Error Handling & Global Exception Middleware

Day 4 focused on centralized handling of unexpected exceptions using custom ASP.NET Core middleware.

Implemented:

- Custom `ExceptionHandlingMiddleware`.
- Centralized handling of unhandled exceptions.
- Standardized `ProblemDetails` responses.
- Structured logging using `ILogger`.
- Request path included as structured logging context.
- Safe `500 Internal Server Error` responses without exposing exception messages or stack traces.
- Middleware registered early in the ASP.NET Core request pipeline.

### Middleware Registration

```csharp
app.UseMiddleware<ExceptionHandlingMiddleware>();
```

### Structured Logging

```csharp
_logger.LogError(
    ex,
    "Unhandled exception occurred while processing {RequestPath}",
    context.Request.Path);
```

### ProblemDetails Response

```json
{
  "title": "An unexpected error occurred.",
  "status": 500
}
```

### Testing

A temporary test endpoint was used to deliberately trigger an unhandled exception.

The test verified that:

- The client receives `500 Internal Server Error`.
- The client receives a standardized `ProblemDetails` response.
- The exception message and stack trace are not exposed to the client.
- The server logs the full exception details.
- The request path is included in the structured log.

The test endpoint was commented out after verification.

### Evidence

Day 4 evidence includes:

- `ExceptionHandlingMiddleware`.
- Middleware registration in `Program.cs`.
- Postman screenshot showing the `500 ProblemDetails` response.
- Server log screenshot showing the exception details and request path.
- Day 4 documentation.

---

## Day 5 — Completed

### Applying Testing to the Project & Week 5 Synthesis

Day 5 focused on applying the testing techniques learned throughout Week 5 to the Cardiac Patient Monitoring System and consolidating the tests into one complete test suite.

The final test suite combines:

- xUnit unit testing.
- Arrange–Act–Assert structure.
- Moq-based service testing.
- EF Core In-Memory testing for service logic.
- Integration testing with `WebApplicationFactory`.
- JWT authentication during integration testing.
- Centralized exception handling with `ProblemDetails`.
- Structured logging with `ILogger`.

### Final Test Project

The final test project contains:

```text
CardiacPatientMonitoringSystem.Tests
├── AppointmentServiceTests.cs
├── CustomWebApplicationFactory.cs
├── PatientsApiIntegrationTests.cs
├── PatientServiceTests.cs
└── VitalSignServiceTests.cs
```

### High-Risk Logic Covered

Three important areas of business logic were selected for focused testing:

#### 1. VitalSignService

```text
GetVitalSignStatus()
```

Covers normal, warning, and critical vital-sign status classification.

#### 2. PatientService

```text
GetByIdAsync()
```

Covers patient existence, ownership validation, and repository failure behavior.

#### 3. AppointmentService

```text
GetByIdAsync()
```

Covers appointment existence and doctor/patient ownership validation.

### AppointmentService Tests

Three tests were added for `AppointmentService.GetByIdAsync()`:

- Doctor owns the appointment and the appointment is returned.
- Appointment does not exist and `null` is returned.
- User is not the owner and `NotOwner` is returned as `true`.

The tests use an isolated Entity Framework Core In-Memory database.

### Final Test Suite

| Test Class | Tests |
|---|---:|
| `VitalSignServiceTests` | 6 |
| `PatientServiceTests` | 4 |
| `AppointmentServiceTests` | 3 |
| `PatientsApiIntegrationTests` | 2 |
| **Total** | **15** |

### Full Test Run

The complete test suite was executed using:

```bash
dotnet test
```

Final result:

```text
Total Tests: 15
Passed:      15
Failed:       0
Skipped:      0
```

**15/15 tests passed successfully.**

### Test Environment

Integration tests continue to use:

```text
Environment: Testing
Database: Entity Framework Core In-Memory
Authentication: JWT
```

This keeps integration testing isolated from the development PostgreSQL database.

### Centralized Error Handling

The centralized error-handling implementation from Day 4 remains part of the final project:

- `ExceptionHandlingMiddleware`.
- Standardized `ProblemDetails` responses.
- Structured logging with `ILogger`.
- Safe `500 Internal Server Error` responses.
- No exposure of exception messages or stack traces to API clients.

### Build Warnings

The final test run completed successfully, but the build reported:

```text
NU1903
Package 'Microsoft.OpenApi' 2.0.0 has a known high severity vulnerability
```

An HTTPS redirection warning was also displayed during the integration test:

```text
Failed to determine the https port for redirect.
```

These warnings did not cause any test failures.

Final result remained:

```text
15 passed
0 failed
```

### Evidence

Day 5 evidence includes the final test run screenshot showing:

- Test Explorer.
- All four test classes.
- `15/15` tests passed.
- Successful test execution.

Evidence file:

```text
Day 5/
└── Evidence/
    └── Day5-Full-Test-Suite.png
```

### Week 5 Synthesis

Week 5 combined:

```text
xUnit
   ↓
Arrange–Act–Assert
   ↓
Moq
   ↓
Integration Testing
   ↓
WebApplicationFactory
   ↓
JWT Authentication
   ↓
Centralized Exception Handling
   ↓
ProblemDetails
   ↓
ILogger
   ↓
Full Test Suite
```

The Cardiac Patient Monitoring System now has a consolidated automated test suite that can be executed with:

```bash
dotnet test
```

### Final Week 5 Result

```text
Day 1  → Completed
Day 2  → Completed
Day 3  → Completed
Day 4  → Completed
Day 5  → Completed
```

**Week 5: ✅ Completed**
