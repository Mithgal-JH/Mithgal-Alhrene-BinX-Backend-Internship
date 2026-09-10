# Cardiac Patient Monitoring System

A backend REST API for managing patients, doctors, medications, appointments, patient medications, and vital signs in a cardiac patient monitoring system.

The project is built with **ASP.NET Core Web API**, **Entity Framework Core**, **PostgreSQL**, and **ASP.NET Core Identity/JWT**.

## Features

- Patient and doctor management
- Medication management
- Patient medication and treatment records
- Appointment management
- Vital signs recording
- JWT authentication
- Role-based authorization
- Input validation with FluentValidation
- Entity Framework Core migrations
- PostgreSQL database
- Repository-based data access
- Patient pagination, search, filtering, and sorting
- Automated unit and integration tests
- Postman API collection
- API, authorization, and API testing documentation

## Roles

The system currently supports three roles:

- **Admin** — full system administration
- **Doctor** — clinical operations and authorized patient-related access
- **Patient** — access to their own data according to authorization rules

Authorization is implemented using ASP.NET Core authentication and role-based rules, with ownership/resource checks documented for patient-related operations.

## Main API Resources

| Resource | Operations |
|---|---|
| Patients | Create, Read, Update, Delete |
| Doctors | Create, Read, Update, Delete |
| Medications | Create, Read, Update, Delete |
| Patient Medications | Create, Read, Update, Delete |
| Appointments | Create, Read, Update, Delete |
| Vital Signs | Create, Read, Delete |

Vital signs do not currently have an update endpoint because they represent historical measurements.

### Patient Query and Access

The Patients API also supports:

- Pagination using `page` and `pageSize`
- Search
- Filtering by gender
- Sorting
- Doctor-specific patient access through `GET /patients/my-patients`
- Patient access to their own data through `GET /patients/my`

## Project Structure

```text
CardiacPatientMonitoringSystem/
├── Controllers/        # API endpoints
├── Data/               # DbContext and database seeding
├── DTOs/               # Request and response models
├── Extensions/         # Service registration/extensions
├── Migrations/         # EF Core database migrations
├── Models/             # Domain entities
├── Repositories/       # Data access layer
│   └── Interfaces/
├── Services/           # Business logic
│   └── Interfaces/
├── Validation/         # FluentValidation validators
├── Postman/            # Postman API collection
├── docs/               # API, authorization, and testing documentation
└── Program.cs          # Application configuration and startup
```

## Technologies

- C#
- ASP.NET Core Web API
- .NET 10
- Entity Framework Core
- PostgreSQL
- ASP.NET Core Identity
- JWT Bearer Authentication
- FluentValidation
- xUnit
- Postman

## Running the Project

### 1. Configure the database

Update the PostgreSQL connection string in:

```text
appsettings.json
```

### 2. Apply migrations

```bash
dotnet ef database update
```

### 3. Run the API

```bash
dotnet run
```

The API runs locally using the configured ASP.NET Core launch settings.

## API Documentation

Detailed endpoint documentation is available in:

```text
docs/API_DOCUMENTATION.md
```

A formatted PDF version is also included in the `docs` folder.

## Authorization Documentation

The project's authorization rules are documented in the final authorization matrix:

```text
docs/Cardiac_Patient_Monitoring_Authorization_Matrix_FINAL.pdf
```

The matrix describes the actual permissions for the supported roles and API resources.

## Postman API Testing Scenario

A documented record of the API testing process, including Postman screenshots and the tested scenarios, is available in:

```text
docs/Cardiac_API_Postman_Scenario_With_Explained_Screenshots.pdf
```

This document provides evidence of the endpoint testing performed during development, including successful CRUD operations and authentication/authorization-related testing.

## Automated Tests

The project includes automated unit and integration tests covering services and API behavior.

Test projects and supporting test infrastructure are included in:

```text
CardiacPatientMonitoringSystem.Tests/
```

## Postman

The project includes a ready-to-use Postman collection:

```text
Postman/Cardiac Patient Monitoring System API.postman_collection.json
```

The collection is organized by API resource and can be used to test the endpoints.

## Database

Database-related documentation and diagrams are available in:

```text
Database/
```

This includes the ERD and normalization documentation.

## Status

The project includes the main API functionality, authentication, authorization, validation, repository-based data access, database migrations, patient query/access features, API documentation, authorization documentation, Postman testing evidence, and automated tests required for the current version of the system.
