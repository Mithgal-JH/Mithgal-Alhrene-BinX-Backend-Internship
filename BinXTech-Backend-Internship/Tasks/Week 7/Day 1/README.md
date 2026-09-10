# Week 7 - Day 1: Sprint 2 Planning & Identity Integration

## Overview

Day 1 of Week 7 focused on preparing Sprint 2 for the Cardiac Patient Monitoring System.

The main goal was to establish the foundation for Authentication and Authorization using ASP.NET Core Identity and define the roles and access rules that will be implemented during the sprint.

---

## Sprint 2 Goal

> Implement Authentication and Authorization in the Cardiac Patient Monitoring System.

The sprint will build on the existing Capstone project from Sprint 1 and introduce secure user authentication, role-based authorization, and resource ownership rules.

---

## Tasks Completed

### 1. Sprint 2 Planning

Defined the main areas that will be covered during Sprint 2:

- Authentication
- User Registration
- Login
- JWT-based authentication
- Role-Based Authorization (RBAC)
- Resource Ownership
- Protected API endpoints

---

### 2. ASP.NET Core Identity Integration

The existing `ApplicationDbContext` already integrates ASP.NET Core Identity:

```csharp
public class ApplicationDbContext : IdentityDbContext<IdentityUser>
```

The context contains both Identity entities and the application's existing entities.

Existing application entities include:

- `Patient`
- `Doctor`
- `VitalSign`
- `Medication`
- `PatientMedication`
- `Appointment`
- `Notification`

Identity is therefore integrated with the existing application database rather than using a separate database.

---

### 3. Identity Roles

The application defines three roles:

```text
Admin
Doctor
Patient
```

These roles will be used later for Role-Based Authorization.

---

### 4. Role Seeding

The `IdentitySeeder` checks whether each role already exists before creating it.

```csharp
if (!await roleManager.RoleExistsAsync(role))
{
    await roleManager.CreateAsync(new IdentityRole(role));
}
```

This ensures that the roles can safely be seeded when the application starts without creating duplicates.

---

### 5. Admin Seeding

An initial Admin account is created using credentials provided through application configuration.

The seeder:

1. Reads the Admin email and password from configuration.
2. Checks whether the Admin account already exists.
3. Creates the account if necessary.
4. Ensures that the account belongs to the `Admin` role.

```csharp
if (!await userManager.IsInRoleAsync(admin, "Admin"))
{
    await userManager.AddToRoleAsync(admin, "Admin");
}
```

---

## Authorization Planning

The main authorization roles are:

| Role | Responsibility |
|---|---|
| Admin | Manage system-level resources |
| Doctor | Access and manage authorized patient data |
| Patient | Access their own data |

Authorization will not depend only on whether a user is authenticated.

The system will also consider **resource ownership**.

For example:

```text
Authenticated User
        ↓
Role Authorization
        ↓
Resource Ownership
        ↓
Allow / Deny Access
```

A Patient should not be able to access another Patient's private data simply because they have a valid JWT.

---

## Current Architecture

```text
ASP.NET Core Web API
        │
        ├── Controllers
        │
        ├── Services
        │
        ├── ApplicationDbContext
        │       │
        │       ├── Application Entities
        │       │
        │       └── ASP.NET Core Identity
        │
        └── SQL Server Database
                │
                ├── Application Tables
                │
                └── Identity Tables
```

---

## Day 1 Outcome

By the end of Day 1:

- [x] Sprint 2 goal defined
- [x] ASP.NET Core Identity integrated
- [x] Identity migration already exists
- [x] `Admin`, `Doctor`, and `Patient` roles defined
- [x] Roles seeding implemented
- [x] Admin seeding implemented
- [x] Authorization responsibilities identified
- [x] Resource ownership identified as an authorization requirement

---

## Key Takeaways

- ASP.NET Core Identity is integrated into the existing application database.
- The system uses three main roles: `Admin`, `Doctor`, and `Patient`.
- Roles and the initial Admin account are seeded automatically.
- Authentication determines **who the user is**.
- Authorization determines **what the user is allowed to do**.
- Resource ownership ensures that users cannot access resources that do not belong to them.

---

## Technologies Used

- C#
- ASP.NET Core Web API
- ASP.NET Core Identity
- Entity Framework Core
- SQL Server
- JWT Authentication
