# Week 7 — Authentication & Authorization

## Week Overview

Week 7 focuses on implementing Authentication and Authorization in the Cardiac Patient Monitoring System.

The week builds on the existing Capstone project and introduces secure user authentication, role-based authorization, and resource ownership rules.

**Status:** 🟡 In Progress  
**Current Day:** Day 1 — Completed  
**Week Status:** In Progress

## Week Objectives

During this week, the main goals are to:

- Implement Authentication in the Cardiac Patient Monitoring System.
- Implement user registration and login.
- Use JWT-based authentication.
- Implement Role-Based Authorization (RBAC).
- Configure and use application roles.
- Protect API endpoints.
- Apply resource ownership rules.
- Prevent users from accessing resources that do not belong to them.

## Daily Progress

| Day | Topic | Status |
|---|---|---|
| Day 1 | Sprint 2 Planning & Identity Integration | ✅ Completed |
| Day 2 | Authentication: Registration, Login & JWT | ⏳ Pending |
| Day 3 | Role-Based Authorization (RBAC) | ⏳ Pending |
| Day 4 | Resource Ownership & Protected Endpoints | ⏳ Pending |
| Day 5 | Sprint Review, Testing & Retrospective | ⏳ Pending |

---

## Day 1 — Completed

### Sprint 2 Planning & Identity Integration

Day 1 focused on preparing Sprint 2 and verifying the existing ASP.NET Core Identity integration in the Cardiac Patient Monitoring System.

The existing Capstone project was used as the starting point, with the Identity foundation already integrated into the application.

### Sprint 2 Goal

> Implement Authentication and Authorization in the Cardiac Patient Monitoring System.

The sprint will cover:

- Authentication
- User Registration
- Login
- JWT-based authentication
- Role-Based Authorization (RBAC)
- Resource Ownership
- Protected API endpoints

### ASP.NET Core Identity Integration

The existing `ApplicationDbContext` integrates ASP.NET Core Identity:

```csharp
public class ApplicationDbContext : IdentityDbContext<IdentityUser>
```

The same database context contains both Identity and application entities.

Existing application entities include:

- `Patient`
- `Doctor`
- `VitalSign`
- `Medication`
- `PatientMedication`
- `Appointment`
- `Notification`

### Identity Roles

The application defines three roles:

```text
Admin
Doctor
Patient
```

These roles will be used for Role-Based Authorization.

### Role Seeding

The existing `IdentitySeeder` checks whether each role exists before creating it.

```csharp
if (!await roleManager.RoleExistsAsync(role))
{
    await roleManager.CreateAsync(new IdentityRole(role));
}
```

This prevents duplicate roles when the seeding process runs.

### Admin Seeding

The `IdentitySeeder` also creates and configures an initial Admin account using credentials from application configuration.

The process:

1. Reads the Admin email and password from configuration.
2. Checks whether the Admin account already exists.
3. Creates the account if necessary.
4. Ensures the account belongs to the `Admin` role.

```csharp
if (!await userManager.IsInRoleAsync(admin, "Admin"))
{
    await userManager.AddToRoleAsync(admin, "Admin");
}
```

### Authorization Planning

The main application roles and responsibilities were identified:

| Role | Responsibility |
|---|---|
| Admin | Manage system-level resources |
| Doctor | Access and manage authorized patient data |
| Patient | Access their own data |

Authorization will consider both the user's role and resource ownership.

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

A Patient should not be able to access another Patient's private data simply because they are authenticated.

### Day 1 Outcome

- [x] Sprint 2 goal defined
- [x] Existing ASP.NET Core Identity integration verified
- [x] Identity migration already exists
- [x] `Admin`, `Doctor`, and `Patient` roles defined
- [x] Role seeding implemented
- [x] Admin seeding implemented
- [x] Authorization responsibilities identified
- [x] Resource ownership identified as an authorization requirement

---

## Week 7 Progress

```text
Day 1  → ✅ Completed
Day 2  → ⏳ Pending
Day 3  → ⏳ Pending
Day 4  → ⏳ Pending
Day 5  → ⏳ Pending
```

**Week 7 — In Progress 🟡**
