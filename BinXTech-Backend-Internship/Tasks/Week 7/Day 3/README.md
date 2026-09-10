# Week 7 — Day 3 — Role-Based Access Control & Ownership

## Overview

Day 3 focused on applying and validating **Role-Based Access Control (RBAC)** and **resource ownership** rules in the Cardiac Patient Monitoring System API.

The practical work was validated through **Postman** using authenticated requests and real API responses.

The goal was to ensure that authentication alone is not enough to access protected resources: the user's role and ownership of the requested resource must also be respected.

---

## Objectives

- Apply role-based authorization to protected API endpoints.
- Distinguish between authenticated access and authorized access.
- Enforce ownership rules for patient-specific resources.
- Validate authorization behavior using Postman.
- Verify appropriate HTTP status codes such as `200 OK` and `403 Forbidden`.

---

## Authorization Model

The Cardiac Patient Monitoring System uses two main roles:

| Role | Purpose |
|---|---|
| **Admin** | Manage and access administrative resources across the system |
| **Patient** | Access and manage resources belonging to the authenticated patient, where permitted |

Ownership is checked separately from the role.

A user having a valid JWT does **not** automatically mean they can access another patient's data.

---

## Hands-on Lab

The Week 7 Day 3 lab requires:

1. Assigning the appropriate role to users.
2. Applying role requirements to project endpoints.
3. Adding ownership checks to endpoints returning user-specific data.
4. Testing rejection of unauthorized role access.
5. Testing that one user's token cannot access another user's specific resource.

The official Week 7 material specifically emphasizes that role checks alone are not sufficient; endpoints returning a specific user's data also need an explicit ownership check.

---

## Postman Validation

### 1. Patient Login

A patient account was authenticated through:

```http
POST /api/auth/login
```

The request returned:

```text
200 OK
```

and a JWT token was issued successfully.

The token was then used as a Bearer token for protected API requests.

---

### 2. Accessing a Patient's Own Record

The authenticated patient accessed their patient record using:

```http
GET /api/patients/15
```

Result:

```text
200 OK
```

The API returned the patient's record successfully.

This demonstrates that an authenticated patient can access an allowed resource belonging to them.

---

### 3. Ownership Protection

A request was made for a different patient:

```http
GET /api/patients/9
```

The API returned:

```text
403 Forbidden
```

This demonstrates the ownership restriction: an authenticated user cannot access another patient's protected record when they do not own it.

---

### 4. Delete Authorization

A delete request was tested against a patient resource:

```http
DELETE /api/patients/1
```

The API returned:

```text
403 Forbidden
```

This confirms that the authenticated patient is not allowed to perform an administrative delete operation.

---

### 5. Protected Resource Access

The following protected resources were also verified through Postman:

```http
GET /api/patientmedications
GET /api/vitalsigns
```

Both requests returned:

```text
200 OK
```

The API correctly responded with valid empty collections where no records were available.

---

## Evidence Summary

| Test | Endpoint | Result |
|---|---|---|
| Patient Login | `POST /api/auth/login` | `200 OK` |
| Own Patient Record | `GET /api/patients/15` | `200 OK` |
| Other Patient Record | `GET /api/patients/9` | `403 Forbidden` |
| Patient Delete Attempt | `DELETE /api/patients/1` | `403 Forbidden` |
| Patient Medications | `GET /api/patientmedications` | `200 OK` |
| Vital Signs | `GET /api/vitalsigns` | `200 OK` |

---

## Key Concepts Practiced

### Authentication vs Authorization

**Authentication** answers:

> Who is the user?

The JWT proves the identity of the authenticated user.

**Authorization** answers:

> What is this user allowed to do?

RBAC and ownership checks determine whether the request should be permitted.

---

### RBAC

Role-based authorization restricts endpoints according to the user's role.

For example:

```csharp
[Authorize(Roles = "Admin")]
```

allows only users with the `Admin` role to access the protected endpoint.

An authenticated user without the required role receives:

```text
403 Forbidden
```

---

### Ownership Authorization

Ownership authorization verifies that the requested resource belongs to the authenticated user.

Conceptually:

```text
Authenticated User
        ↓
Check requested resource
        ↓
Does resource belong to user?
     ↙           ↘
   Yes            No
    ↓              ↓
  Allow          Reject
```

This prevents unauthorized access to another patient's private data.

---

## Result

Day 3 successfully covered the practical RBAC and ownership validation required for the Cardiac Patient Monitoring System.

The Postman evidence demonstrates:

- Successful JWT authentication.
- Successful access to an allowed patient resource.
- Rejection of access to another patient's resource.
- Rejection of an unauthorized delete operation.
- Successful access to protected collection endpoints.

**Status: ✅ Day 3 Completed**

---

## Tools Used

- ASP.NET Core
- ASP.NET Core Identity
- JWT Authentication
- Role-Based Authorization
- Ownership Authorization
- Postman
- Cardiac Patient Monitoring System API

---

## Reference

The Week 7 curriculum defines the Day 3 focus as:

> Role-based access control across customer and admin roles.

The Hands-on Lab requires applying role requirements, adding ownership checks, and validating rejected unauthorized requests through Postman.
