# Week 5 - Day 2
## Unit Testing with Moq

### Objective

The goal of Day 2 was to practice mocking dependencies using **Moq** and **xUnit**.

The `PatientService` was refactored to depend on a repository interface instead of directly accessing `ApplicationDbContext`.

This allows the service to be tested independently without using the real database.

---

## Architecture

Before:

```text
PatientService
      ↓
ApplicationDbContext
      ↓
Database
```

After introducing the Repository Pattern:

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

This isolates the service from the database.

---

## Repository Pattern

### IPatientRepository

The repository interface defines the operations available for patient data access:

- `GetAllAsync()`
- `GetByIdAsync()`
- `AddAsync()`
- `UpdateAsync()`
- `DeleteAsync()`

### PatientRepository

`PatientRepository` implements `IPatientRepository` and is responsible for interacting with `ApplicationDbContext`.

The service no longer accesses the database directly.

---

## Moq Tests

Four unit tests were implemented for `PatientService.GetByIdAsync()`.

### 1. Patient Exists and User Is Owner

The repository mock returns a specific patient.

The test verifies that the service:

- Returns the patient.
- Sets `NotOwner` to `false`.
- Processes the returned data correctly.

Moq:

```csharp
mockRepository
    .Setup(r => r.GetByIdAsync(1))
    .ReturnsAsync(patient);
```

---

### 2. Patient Does Not Exist

The repository mock returns `null`.

The test verifies that the service returns:

```text
Patient = null
NotOwner = false
```

Moq:

```csharp
mockRepository
    .Setup(r => r.GetByIdAsync(999))
    .ReturnsAsync((Patient?)null);
```

---

### 3. Patient Belongs to Another User

The repository mock returns a patient belonging to another user.

The test verifies that:

```text
Patient = null
NotOwner = true
```

This tests the ownership logic inside the service.

---

### 4. Repository Throws an Exception

The repository mock is configured to throw an exception.

Moq:

```csharp
mockRepository
    .Setup(r => r.GetByIdAsync(1))
    .ThrowsAsync(new Exception("Database error"));
```

The test verifies that the exception is propagated correctly.

---

## Verify

Moq `Verify()` was used to confirm that the repository method was called exactly once.

Example:

```csharp
mockRepository.Verify(
    r => r.GetByIdAsync(1),
    Times.Once);
```

This confirms that `GetByIdAsync(1)` was called exactly one time by the service.

---

## Test Results

All tests passed successfully.

```text
Total Tests: 4
Passed: 4
Failed: 0
Skipped: 0
Result: SUCCEEDED
```

The tests were executed using xUnit through the .NET test runner.

---

## Tools Used

- C#
- ASP.NET Core
- xUnit
- Moq
- Entity Framework Core
- Repository Pattern

---

## Conclusion

Day 2 demonstrated how to isolate service-layer logic from database access by introducing a repository abstraction and mocking the repository dependency with Moq.

The tests covered:

- Mocked return values
- Missing data
- Ownership validation
- Repository exceptions
- Repository call verification

All Day 2 tests passed successfully.
