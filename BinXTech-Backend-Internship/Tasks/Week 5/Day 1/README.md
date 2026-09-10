# Week 5 — Day 1

## Project

**CardioCare — Cardiac Care & Monitoring Platform**

The existing Cardiac Patient Monitoring System from Weeks 1–4 is used as the foundation for the Phase 3 project.

The Day 1 scope focuses the project on cardiac monitoring and follow-up rather than building a full hospital management system.

## Scope

> CardioCare is an ASP.NET Core REST API designed to support cardiac patient monitoring and follow-up through patient profiles, vital-sign records, medication tracking, appointments, health trends, and alerts. The system will provide JWT-based authentication, role-based and resource-based authorization, validation, relational data management with Entity Framework Core, documented REST endpoints, and automated testing. By the end of Phase 3, the project will target the BinX Tech professional backend baseline, including unit and integration testing, centralized error handling, Postman and Swagger documentation, CI/CD, and deployment.

The scope is designed to reach the professional backend baseline by Week 9.

## xUnit Test Project

A separate xUnit project was created:

```text
CardiacPatientMonitoringSystem.Tests
```

It references the main API project:

```text
CardiacPatientMonitoringSystem
```

A separate test project keeps test code isolated from the application code and allows the test suite to be run independently.

## Unit Testing

For Day 1, a pure business-logic method was added to `VitalSignService`:

```text
GetVitalSignStatus()
```

It evaluates the supplied vital-sign values and returns:

```text
Normal
Warning
Critical
```

The method does not access the database, so no mocking is required for this test.

### Tests

The required Day 1 tests were implemented:

- 3 `[Fact]` tests.
- 1 `[Theory]` test with 3 input cases.
- Tests follow the Arrange–Act–Assert pattern.

## Test Result

The full test suite was executed with:

```bash
dotnet test
```

Result:

```text
Total Tests: 6
Passed: 6
Failed: 0
Skipped: 0
Result: SUCCEEDED
```

## Evidence

Recommended screenshots:

- `scope.png` — project scope.
- `xunit-tests.png` — test project and test code.
- `tests-6-passed.png` — successful test run.

## Note

`dotnet test` completed successfully.

An existing `NU1903` warning was reported for `Microsoft.OpenApi 2.0.0`. It did not affect the test result.

## Day 1 Status

- [x] Project scope defined
- [x] xUnit test project created
- [x] API project referenced
- [x] 3 Fact tests written
- [x] 1 Theory with 3 cases written
- [x] All 6 tests passed
- [x] Test evidence captured
