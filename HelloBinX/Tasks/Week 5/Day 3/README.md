# Week 5 - Day 3: Integration Testing

## Overview

Implemented integration testing for the Cardiac Patient Monitoring System API using `WebApplicationFactory` and xUnit.

## What We Implemented

### 1. WebApplicationFactory
Configured `WebApplicationFactory` to run the API inside the integration tests without starting the server manually.

### 2. Isolated Test Database
Configured the application to use an Entity Framework Core In-Memory database when running in the `Testing` environment.

This keeps integration tests separate from the development PostgreSQL database.

### 3. Custom Test Factory
Created `CustomWebApplicationFactory` to:
- Use the `Testing` environment.
- Provide test-specific JWT configuration.
- Create the In-Memory database.
- Seed a Patient role.
- Create the test patient user.
- Seed Patient ID `10`.

Test credentials:
- Email: `patient17@example.com`
- Password: `Patient@123`

### 4. Happy Path Integration Test
Tested:

`GET /api/Patients/10`

The test:
- Logs in using the test patient account.
- Attaches the returned JWT as a Bearer token.
- Sends the request to the protected endpoint.
- Verifies `200 OK`.
- Verifies values from the response body, including `patientId` and `email`.

### 5. Not Found Integration Test
Tested:

`GET /api/Patients/99999`

The test authenticates with a valid JWT and verifies that the API returns:

`404 NotFound`

### 6. JWT Authentication
The tests use the real login endpoint:

`POST /api/Auth/login`

The returned JWT is attached to the request using:

`Authorization: Bearer <token>`

This verifies the protected endpoint through the actual authentication pipeline.

## Test Result

```text
Total Tests: 2
Passed: 2
Failed: 0
Skipped: 0
```

## Technologies

- ASP.NET Core
- xUnit
- Microsoft.AspNetCore.Mvc.Testing
- Entity Framework Core In-Memory
- JWT Authentication
- PostgreSQL (development database)

## Day 3 Status

Completed successfully.
