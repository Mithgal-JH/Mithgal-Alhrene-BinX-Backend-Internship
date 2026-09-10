# Week 7 — Authentication & Authorization

## Week Overview

Week 7 focuses on implementing Authentication and Authorization in the Cardiac Patient Monitoring System.

The week builds on the existing Capstone project and introduces secure user authentication, role-based authorization, resource ownership rules, and custom middleware.

**Status:** 🟢 Completed  
**Current Day:** Day 5 — Completed  
**Week Status:** Completed

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
- Implement custom middleware for cross-cutting concerns.

## Daily Progress

| Day | Topic | Status |
|---|---|---|
| Day 1 | Sprint 2 Planning & Identity Integration | ✅ Completed |
| Day 2 | Authentication: Registration, Login & JWT | ✅ Completed |
| Day 3 | Role-Based Authorization (RBAC) | ✅ Completed |
| Day 4 | Custom Middleware & Cross-Cutting Concerns | ✅ Completed |
| Day 5 | Sprint Review, Testing & Retrospective | ✅ Completed |

---

## Day 1 — Completed

### Sprint 2 Planning & Identity Integration

Day 1 focused on preparing Sprint 2 and verifying the existing ASP.NET Core Identity integration in the Cardiac Patient Monitoring System.

### Sprint 2 Goal

> Implement Authentication and Authorization in the Cardiac Patient Monitoring System.

### ASP.NET Core Identity Integration

The existing `ApplicationDbContext` integrates ASP.NET Core Identity:

```csharp
public class ApplicationDbContext : IdentityDbContext<IdentityUser>
```

The application defines three roles:

```text
Admin
Doctor
Patient
```

Role and Admin seeding were verified, and authorization responsibilities were planned around both role-based access and resource ownership.

### Day 1 Outcome

- [x] Sprint 2 goal defined
- [x] ASP.NET Core Identity integration verified
- [x] Identity roles defined
- [x] Role seeding implemented
- [x] Admin seeding implemented
- [x] Authorization responsibilities identified
- [x] Resource ownership identified as an authorization requirement

---

## Day 2 — Completed

### Authentication: Registration, Login & JWT

Day 2 focused on implementing and validating authentication using ASP.NET Core Identity and JWT.

### Authentication Flow

```text
Client
   ↓
AuthController
   ↓
AuthService
   ↓
ASP.NET Core Identity
   ↓
JWT Token
```

### Implemented Features

- User registration.
- User login.
- Password verification using ASP.NET Core Identity.
- JWT token generation.
- JWT Bearer Authentication.
- User ID and Email claims.
- Issuer and Audience configuration.
- Token signing and expiration.

### Login Endpoint

```http
POST /api/auth/login
```

Successful authentication returns:

```text
200 OK
```

with a JWT token.

Invalid credentials are rejected with:

```text
401 Unauthorized
```

JWT claims and token expiration were also verified during testing.

### Day 2 Outcome

- [x] Registration flow implemented
- [x] Login flow implemented
- [x] JWT generation implemented
- [x] JWT Bearer Authentication configured
- [x] JWT claims verified
- [x] Invalid login rejected
- [x] Token expiration verified

---

## Day 3 — Completed

### Role-Based Authorization (RBAC) & Ownership Validation

Day 3 focused on validating Role-Based Authorization and resource ownership behavior using Postman with authenticated requests.

### Authentication Validation

A patient account was authenticated through:

```http
POST /api/auth/login
```

Result:

```text
200 OK
```

The returned JWT was used as a Bearer Token for protected API requests.

### Authorization & Ownership Tests

| Test | Endpoint | Result |
|---|---|---|
| Patient Login | `POST /api/auth/login` | `200 OK` |
| Access Patient Record | `GET /api/patients/15` | `200 OK` |
| Access Another Patient | `GET /api/patients/9` | `403 Forbidden` |
| Delete Patient | `DELETE /api/patients/1` | `403 Forbidden` |
| Get Patient Medications | `GET /api/patientmedications` | `200 OK` |
| Get Vital Signs | `GET /api/vitalsigns` | `200 OK` |

### Ownership Validation

The authenticated patient could access their allowed patient resource:

```http
GET /api/patients/15
```

Result:

```text
200 OK
```

Access to another patient's protected resource:

```http
GET /api/patients/9
```

Result:

```text
403 Forbidden
```

This confirms that authentication alone does not grant access to another patient's private data.

### Role-Based Access Validation

The patient attempted:

```http
DELETE /api/patients/1
```

Result:

```text
403 Forbidden
```

This confirms that the Patient role cannot perform the protected administrative delete operation.

### Day 3 Outcome

- [x] JWT authentication verified
- [x] Protected endpoints verified
- [x] Own-resource access verified
- [x] Unauthorized resource access rejected
- [x] Unauthorized delete operation rejected
- [x] RBAC and ownership behavior documented

---

## Day 4 — Completed

### Custom Middleware & Request Timing

Day 4 focused on implementing a custom ASP.NET Core Middleware for measuring API request execution time.

The goal was to apply a genuine cross-cutting concern without duplicating timing logic inside Controllers.

### RequestTimingMiddleware

A custom `RequestTimingMiddleware` was implemented using:

- `RequestDelegate`
- `ILogger<RequestTimingMiddleware>`
- `Stopwatch`
- `try/finally`

The Middleware starts timing before passing the request to the next pipeline component:

```csharp
var stopwatch = Stopwatch.StartNew();

try
{
    await _next(context);
}
finally
{
    stopwatch.Stop();

    _logger.LogInformation(
        "HTTP {Method} {Path} responded {StatusCode} in {ElapsedMilliseconds} ms",
        context.Request.Method,
        context.Request.Path,
        context.Response.StatusCode,
        stopwatch.ElapsedMilliseconds);
}
```

### Structured Logging

The Middleware records:

- HTTP Method
- Request Path
- Response Status Code
- Request Execution Time

Example:

```text
HTTP GET /api/patientmedications responded 200 in 27 ms
```

### Middleware Ordering

The existing `ExceptionHandlingMiddleware` is placed before `RequestTimingMiddleware`.

Conceptually:

```text
HTTP Request
     ↓
ExceptionHandlingMiddleware
     ↓
RequestTimingMiddleware
     ↓
Authentication / Authorization
     ↓
Controller
     ↓
HTTP Response
     ↑
RequestTimingMiddleware
     ↑
ExceptionHandlingMiddleware
```

Using `try/finally` in the timing middleware ensures that the timing log is executed when the downstream pipeline completes, including exception scenarios.

### Testing

The application was successfully built and executed using:

```powershell
dotnet build
dotnet run
```

The Middleware was tested across multiple API endpoints.

Example results:

```text
HTTP POST /api/auth/login responded 200 in 954 ms

HTTP GET /api/patientmedications/2 responded 200 in 361 ms

HTTP GET /api/patientmedications/2 responded 200 in 18 ms

HTTP GET /api/patientmedications responded 200 in 27 ms
```

These results confirm that the Middleware is applied globally across different API requests.

### Day 4 Outcome

- [x] Custom `RequestTimingMiddleware` implemented
- [x] Typed `ILogger<RequestTimingMiddleware>` configured
- [x] Request timing implemented using `Stopwatch`
- [x] `try/finally` used for reliable timing logging
- [x] Structured logging implemented
- [x] HTTP method, path, status code, and execution time logged
- [x] Middleware ordering verified
- [x] Multiple API endpoints tested successfully

---

## Day 5 — Completed

### Sprint 2 Close-Out

Day 5 closed Sprint 2 through the final Postman demonstration, Sprint Review, and Retrospective.

### Postman Demo

The complete authentication and authorization flow was demonstrated:

| # | Request | Result |
|---|---|---|
| 1 | Register Patient | `200 OK` |
| 2 | Patient Login | `200 OK` |
| 3 | Get Own Patient Record | `200 OK` |
| 4 | Get Another Patient Record | `403 Forbidden` |
| 5 | Patient Attempts Delete | `403 Forbidden` |
| 6 | Admin Login | `200 OK` |
| 7 | Admin Deletes Patient | `204 No Content` |

### Authorization Rejection Cases

Two deliberate authorization rejection cases were demonstrated:

- Patient attempting to access another patient's record → `403 Forbidden`
- Patient attempting to delete a patient record → `403 Forbidden`

These cases confirm that authorization is not only allowing valid requests but also correctly rejecting unauthorized operations.

### Sprint 2 Retrospective

#### What Went Well

- JWT authentication was successfully demonstrated for Patient and Admin users.
- RBAC behavior was verified through real API requests.
- Patient ownership restrictions were successfully tested.
- Protected Admin functionality was successfully demonstrated.
- Postman evidence was captured for Sprint 2 close-out.

#### What Could Be Improved

Keep the Postman collection and supporting documentation synchronized with implementation changes throughout the sprint so final verification is faster and easier.

#### Action for Sprint 3

Continue applying a verification-first approach by keeping API testing and evidence aligned with each important feature implemented in the next sprint.

### Day 5 Outcome

- [x] Sprint 2 authentication and authorization demo completed
- [x] Patient JWT authentication verified
- [x] Admin JWT authentication verified
- [x] Ownership rejection verified
- [x] RBAC rejection verified
- [x] Admin protected operation verified
- [x] Postman evidence captured
- [x] Sprint 2 retrospective completed
- [x] Sprint 2 close-out completed

---

## Week 7 Progress

```text
Day 1  → ✅ Completed
Day 2  → ✅ Completed
Day 3  → ✅ Completed
Day 4  → ✅ Completed
Day 5  → ✅ Completed
```

**Week 7 — Completed 🟢**

## Week 7 Summary

Week 7 completed the Authentication & Authorization phase of the Cardiac Patient Monitoring System.

The week covered:

- ASP.NET Core Identity integration
- User registration and login
- JWT-based authentication
- Role-Based Authorization (RBAC)
- Resource ownership validation
- Protected API endpoints
- Custom middleware
- Request timing and structured logging
- Sprint 2 Postman demonstration
- Sprint 2 retrospective

**Week 7 is complete.**

> **Note:** The Notion deliverable was cancelled and is not included in the Week 7 workflow.
