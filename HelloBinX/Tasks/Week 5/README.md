# Week 5 — Testing & Error Handling; Project Begins

## Week Overview

Week 5 marks the transition from Phase 2 into Phase 3.

The main focus of the week is **testing with xUnit and Moq, integration testing, centralized error handling, and beginning the applied project**.

**Status:** 🟡 In Progress  
**Current Day:** Day 1 — Completed  
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
| Day 2 | Mocking Dependencies with Moq | ⏳ Not Started |
| Day 3 | Integration Testing with WebApplicationFactory | ⏳ Not Started |
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

## Next

Day 2 will continue with **Moq and dependency mocking**.

More sections will be added to this README as the remaining days of Week 5 are completed.
