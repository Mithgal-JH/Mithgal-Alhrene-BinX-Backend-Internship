# CardioCare — Phase 3 Project Scope

## BinX Tech Backend .NET Internship — Week 5 Day 1

**Project:** CardioCare — Cardiac Care & Monitoring Platform  
**Project Type:** Healthcare Management API  
**Technology:** ASP.NET Core / C# / Entity Framework Core

---

## 1. Project Overview

**CardioCare** is a backend platform focused on cardiac patient monitoring and follow-up.

The system allows patients to record health measurements, manage their medications and appointments, while doctors can monitor assigned patients, review health trends, and respond to important alerts.

The existing Cardiac Patient Monitoring System developed during the earlier internship phase will be used as the technical foundation and domain starting point. During Phase 3, the project will be refined and extended to deliver a stronger, testable, and production-oriented backend.

---

## 2. Three-Sentence Scope Statement

> **CardioCare is an ASP.NET Core REST API designed to support cardiac patient monitoring and follow-up through patient profiles, vital-sign records, medication tracking, appointments, health trends, and alerts. The system will provide JWT-based authentication, role-based and resource-based authorization, validation, relational data management with Entity Framework Core, documented REST endpoints, and automated testing. By the end of Phase 3, the project will target the BinX Tech professional backend baseline, including unit and integration testing, centralized error handling, Postman and Swagger documentation, CI/CD, and deployment.**

---

## 3. Main Roles

### Patient
- Manage personal profile.
- Record health measurements.
- View personal health history and trends.
- View assigned medications.
- Record medication adherence.
- View appointments and alerts.

### Doctor
- View assigned patients.
- Review patient health measurements.
- Review health trends.
- Review medication adherence.
- Review and manage alerts.
- Manage patient appointments.
- Add follow-up notes where applicable.

### Admin
- Manage users and system-level configuration.
- Manage doctors and patient/provider relationships.
- Perform administrative operations according to authorization policies.

---

## 4. Core Modules

### 4.1 Authentication & Users
- Registration/login.
- JWT authentication.
- Role-based authorization.
- Protected endpoints.
- Resource/ownership checks.

### 4.2 Patient Profiles
- Patient profile management.
- Secure access to personal health information.
- Relationship with assigned doctor/provider.

### 4.3 Vital Monitoring
Patients can record measurements such as:
- Heart rate.
- Systolic blood pressure.
- Diastolic blood pressure.
- Oxygen saturation.
- Respiratory rate.
- Temperature.
- Weight.
- Recording date/time.
- Notes.

The module will support historical measurements and querying/filtering measurements by relevant criteria.

### 4.4 Alerts
The system can evaluate recorded measurements against configured thresholds.

Example flow:

```text
Vital Reading
     ↓
Validation
     ↓
Threshold Evaluation
     ↓
Normal ──────────→ Save Reading
     │
     └── Abnormal → Create Alert
```

Alerts may include:
- Type.
- Severity.
- Status.
- Related patient.
- Creation/resolution timestamps.

### 4.5 Medication Tracking
- Medication records.
- Patient medication assignments.
- Dosage and frequency.
- Start/end dates.
- Medication status.
- Medication adherence records.

The system may calculate an adherence rate from recorded medication events.

### 4.6 Appointments
- Patient/doctor appointments.
- Appointment date and time.
- Appointment type.
- Status.
- Reason/notes.
- Validation of conflicting appointments where applicable.

### 4.7 Health Trends
The API will provide aggregated health information based on recorded measurements.

Examples:
- Average heart rate.
- Average blood pressure.
- Average oxygen saturation.
- Measurement counts.
- Date-range filtering.

---

## 5. Database Scope

The project will use a relational database with Entity Framework Core.

The final schema will be refined during Phase 3, but the main domain relationships are expected to follow:

```text
Patient
 ├── Vital Measurements
 ├── Appointments ───── Doctor
 ├── Medications ────── Medication
 └── Alerts
```

The database will use:
- EF Core migrations.
- Foreign-key relationships.
- Appropriate indexes.
- Seed/development data where required.
- A documented ERD.

---

## 6. API Scope

The backend will follow REST principles and use:
- Controllers.
- DTOs.
- Services.
- Dependency Injection.
- Async/Await.
- LINQ.
- Standard HTTP methods and status codes.
- Swagger/OpenAPI documentation.

The exact endpoint list and contracts will be refined during the first Phase 3 sprint.

---

## 7. Security Scope

The system will implement:
- JWT authentication.
- Role-based authorization.
- Resource/ownership authorization.
- Secure password handling.
- Protected healthcare-related resources.
- Appropriate `401 Unauthorized` and `403 Forbidden` responses.

A valid token alone will not grant access to resources that the user is not authorized to access.

---

## 8. Validation & Error Handling

The project will include:
- Request validation.
- Business-rule validation.
- Centralized exception handling.
- Consistent error responses using ProblemDetails.
- Appropriate HTTP status codes.
- Logging through the ASP.NET Core logging infrastructure.

---

## 9. Testing Scope

### Unit Testing
- xUnit.
- Moq.
- Arrange–Act–Assert.
- Tests for important business logic.
- Success and failure scenarios.

### Integration Testing
- WebApplicationFactory.
- HTTP-level API testing.
- Happy-path scenarios.
- Error-path scenarios.
- At least one authenticated endpoint scenario.

The complete test suite should be executable using:

```bash
dotnet test
```

---

## 10. API Documentation & Verification

The project will provide:

### Swagger / OpenAPI
- Documented endpoints.
- Request/response models.
- Authentication information.

### Postman
- Organized collection.
- At least one test per endpoint.
- Happy-path coverage.
- Relevant error-path coverage.

---

## 11. Performance & Advanced Features

Where justified by the project requirements, Phase 3 may include:
- Pagination.
- Filtering and sorting.
- Efficient LINQ queries.
- Database indexing.
- Avoidance of unnecessary database queries.
- Redis caching for suitable frequently accessed data.

These features will only be introduced when they provide real value to the system.

---

## 12. Deployment & CI/CD

The final project will target:
- Public API deployment using Azure App Service or Railway.
- GitHub Actions CI/CD.
- Automated build and test execution.
- Secure environment variables/secrets.
- Deployment after successful CI validation.

---

## 13. Out of Scope

The project is **not intended to become a full hospital management system**.

The following are outside the intended scope unless specifically required later:
- Hospital administration.
- Billing and insurance.
- Laboratory management.
- Pharmacy management as a standalone system.
- Inpatient/bed management.
- Real hospital integrations.
- Real patient/medical data.
- Microservices architecture.
- Message brokers.
- Complex distributed infrastructure.
- A frontend application as part of the backend capstone.

---

## 14. Scope Evolution

This document defines the initial Phase 3 direction.

The exact entities, endpoints, business rules, authorization policies, and advanced features will be refined during Sprint 1 based on:
- Project requirements.
- Mentor feedback.
- Technical feasibility.
- Internship milestones.

The project should remain focused and avoid unnecessary complexity.

---

## 15. Professional Backend Baseline

By the end of Phase 3, CardioCare should target the BinX Tech baseline:

- Fully documented REST API.
- Swagger/OpenAPI.
- Complete Postman collection.
- At least one Postman test per endpoint.
- Relational database.
- EF Core migrations.
- Documented ERD.
- JWT authentication.
- RBAC with at least two roles.
- Unit tests using xUnit and Moq.
- Integration tests using WebApplicationFactory.
- Critical-path test coverage.
- Centralized error handling.
- Deployment to Azure App Service or Railway.
- GitHub Actions CI/CD.
- Portfolio-ready README.

---

## 16. Phase 3 Roadmap Alignment

| Stage | Focus |
|---|---|
| Week 5 | Project kickoff, scope, xUnit, Moq, integration testing, error handling |
| Week 6 | Sprint 1: domain/database/API refinement |
| Week 7 | Sprint 2: authentication, authorization, middleware |
| Week 8 | Sprint 3: queries, performance, caching where justified |
| Week 9 | Sprint 4: testing, documentation, CI/CD, deployment |
| Week 10 | Final evaluation and presentation |

---

## 17. Scope Boundary

### CardioCare IS

A focused cardiac care and monitoring backend that connects patients and doctors through health measurements, medication tracking, appointments, trends, and alerts.

### CardioCare IS NOT

A complete hospital information system or a production medical platform handling real patient data.

---

**Scope Status:** Initial Phase 3 Scope  
**Day:** Week 5 — Day 1  
**Project Foundation:** Existing Cardiac Patient Monitoring System
