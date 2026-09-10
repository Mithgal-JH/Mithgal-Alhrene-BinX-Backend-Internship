# Week 5 --- Day 5: Applying Testing to the Project & Week 5 Synthesis

## Overview

Day 5 focused on applying the testing techniques learned throughout Week
5 to the Cardiac Patient Monitoring System and consolidating the test
suite into a single project.

The final test suite combines:

-   xUnit unit testing.
-   Arrange--Act--Assert testing structure.
-   Moq-based service testing.
-   EF Core In-Memory testing for service logic.
-   Integration testing with `WebApplicationFactory`.
-   JWT authentication during integration testing.
-   Centralized exception handling with `ProblemDetails`.
-   Structured logging with `ILogger`.

The goal was to verify critical project behavior and finish the week
with one complete test suite that can be executed using:

``` bash
dotnet test
```

## Project Structure

The final Day 5 project contains:

``` text
Cardiac-Patient-Monitoring-System/
│
├── CardiacPatientMonitoringSystem/
│
└── CardiacPatientMonitoringSystem.Tests/
    │
    ├── AppointmentServiceTests.cs
    ├── CustomWebApplicationFactory.cs
    ├── PatientsApiIntegrationTests.cs
    ├── PatientServiceTests.cs
    └── VitalSignServiceTests.cs
```

## Testing Areas

### 1. VitalSignService

The existing `VitalSignServiceTests` from Day 1 were included in the
final test suite.

The tests cover the cardiac vital-sign status classification logic
implemented in:

``` text
GetVitalSignStatus()
```

Test cases include:

-   Normal vital signs.
-   Warning conditions.
-   Critical conditions.
-   Multiple scenarios using `[Theory]` and `[InlineData]`.

### Test Count

``` text
6 tests
```

## 2. PatientService with Moq

The `PatientServiceTests` from Day 2 were included in the final test
suite.

The service uses:

``` text
IPatientRepository
```

and the tests isolate the service using:

``` text
Moq
```

The tests cover:

-   Patient exists and the authenticated user is the owner.
-   Patient does not exist.
-   Patient belongs to another user.
-   Repository throws an exception.

Moq features used include:

-   `Mock<T>`
-   `Setup()`
-   `ReturnsAsync()`
-   `ThrowsAsync()`
-   `Verify()`
-   `Times.Once`

### Test Count

``` text
4 tests
```

## 3. AppointmentService

A third high-risk business-logic area was added during Day 5.

The selected method was:

``` text
AppointmentService.GetByIdAsync()
```

This method contains important ownership and access-control logic for
appointments.

The tests cover:

### Doctor owns the appointment

``` text
Doctor user
    ↓
Owns appointment
    ↓
Appointment returned
```

### Appointment does not exist

``` text
Invalid appointment ID
    ↓
Appointment = null
    ↓
NotOwner = false
```

### User is not the owner

``` text
Different user
    ↓
Appointment = null
    ↓
NotOwner = true
```

The tests use an isolated Entity Framework Core In-Memory database.

### Test Count

``` text
3 tests
```

## 4. Integration Testing

The integration tests created during Day 3 were included in the final
test suite.

The integration test setup uses:

-   xUnit.
-   `WebApplicationFactory`.
-   Entity Framework Core In-Memory.
-   JWT authentication.
-   A dedicated `Testing` environment.

The tests verify the protected Patient API endpoint.

### Test Scenarios

#### Existing Patient

``` http
GET /api/Patients/10
```

Expected result:

``` text
200 OK
```

#### Patient Does Not Exist

``` http
GET /api/Patients/99999
```

Expected result:

``` text
404 Not Found
```

### Test Count

``` text
2 tests
```

# Final Test Suite

The complete test suite combines all testing areas from Week 5.

  Test Class                         Tests
  ------------------------------- --------
  `VitalSignServiceTests`                6
  `PatientServiceTests`                  4
  `AppointmentServiceTests`              3
  `PatientsApiIntegrationTests`          2
  **Total**                         **15**

## Full Test Run

The complete test suite was executed using:

``` bash
dotnet test
```

### Result

``` text
Total Tests: 15
Passed:      15
Failed:       0
Skipped:      0
```

Final result:

``` text
15/15 tests passed
```

The full test suite completed successfully.

## High-Risk Logic Covered

The Day 5 test suite covers three important areas of business logic:

### 1. VitalSignService

``` text
GetVitalSignStatus()
```

Tests cardiac vital-sign status classification.

### 2. PatientService

``` text
GetByIdAsync()
```

Tests patient existence and ownership validation.

### 3. AppointmentService

``` text
GetByIdAsync()
```

Tests appointment existence and doctor/patient ownership validation.

These areas were selected because they contain important application
behavior and access-control logic.

## Centralized Error Handling

The centralized error-handling implementation from Day 4 remains part of
the final project.

It includes:

-   `ExceptionHandlingMiddleware`.
-   `ProblemDetails`.
-   Structured logging using `ILogger`.
-   Safe `500 Internal Server Error` responses.
-   No exposure of exception messages or stack traces to API clients.

Example response:

``` json
{
  "title": "An unexpected error occurred.",
  "status": 500
}
```

The temporary exception-testing endpoint used during Day 4 was
removed/commented out after verification.

## Test Environment

Integration testing uses:

``` text
Environment: Testing
Database: Entity Framework Core In-Memory
Authentication: JWT
```

This keeps integration tests isolated from the development PostgreSQL
database.

## Build Warnings

The final test run completed successfully, but the build reported a
package vulnerability warning:

``` text
NU1903
Package 'Microsoft.OpenApi' 2.0.0 has a known high severity vulnerability
```

The warning appeared in both the API and test projects.

It did not cause any test failures.

An HTTPS redirection warning was also displayed during the integration
test:

``` text
Failed to determine the https port for redirect.
```

The integration tests still completed successfully.

These warnings did not affect the final test result:

``` text
15 passed
0 failed
```

## Evidence

Day 5 evidence includes the final test run showing all 15 tests passing.

``` text
Evidence/
└── Day5-Full-Test-Suite.png
```

The screenshot shows:

-   Test Explorer.
-   All four test classes.
-   `15/15` tests passed.
-   Successful test execution.

## Week 5 Synthesis

Week 5 combined the following testing and reliability practices:

``` text
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

The Cardiac Patient Monitoring System now has a consolidated automated
test suite that can be executed with:

``` bash
dotnet test
```

### Final Result

``` text
15 Tests
15 Passed
0 Failed
0 Skipped
```

## Status

**Day 5: Completed**

**Week 5: Completed**
