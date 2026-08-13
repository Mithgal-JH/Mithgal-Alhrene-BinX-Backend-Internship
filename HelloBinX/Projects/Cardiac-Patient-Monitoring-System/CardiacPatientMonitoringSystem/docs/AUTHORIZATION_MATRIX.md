# Authorization Matrix

## 1. Overview

This document defines the planned authorization rules for the **Cardiac
Patient Monitoring System**.

The system currently uses three application roles:

- `Admin`
- `Doctor`
- `Patient`

Authorization is designed in three levels:

1.  **Role-Based Authorization** for straightforward role permissions.
2.  **Policy-Based Authorization** for general business requirements
    such as minimum age.
3.  **Resource-Based Authorization** for ownership and relationship
    checks, such as allowing a doctor to access only patients assigned
    to that doctor.

> **Design note:** The rules in this document are project design
> decisions. They are intended to guide implementation and keep
> authorization behavior consistent across the API.

---

## 2. Roles

---

Role Responsibility

---

`Admin` Full system administration and
management of system resources.

`Doctor` Clinical operations and access to
patient-related data according to
authorization and ownership rules.

`Patient` Access to the patient's own profile
and health-related resources,
subject to ownership and business
rules.

---

---

## 3. Role-Based Authorization Matrix

Legend:

- ✅ Allowed
- ❌ Not allowed
- `*` Requires an additional ownership/resource check

Resource / Action Admin Doctor Patient

---

View Doctors ✅ ✅ ✅
Create Doctor ✅ ❌ ❌
Update Doctor ✅ ❌ ❌
Delete Doctor ✅ ❌ ❌
View Patients ✅ ✅\* ✅\*
Create Patient ✅ ❌ ✅
Update Patient ✅ ✅\* ✅\*
Delete Patient ✅ ❌ ❌
View Vital Signs ✅ ✅\* ✅\*
Create Vital Sign ✅ ✅\* ✅\*
Update Vital Sign ✅ ✅\* ❌
Delete Vital Sign ✅ ❌ ❌
View Medications ✅ ✅ ✅
Create Medication ✅ ✅ ❌
Update Medication ✅ ✅ ❌
Delete Medication ✅ ❌ ❌
View Patient Medications ✅ ✅\* ✅\*
Create Patient Medication ✅ ✅\* ❌
Update Patient Medication ✅ ✅\* ❌
Delete Patient Medication ✅ ❌ ❌
View Appointments ✅ ✅\* ✅\*
Create Appointment ✅ ✅\* ✅\*
Update Appointment ✅ ✅\* ✅\*
Delete Appointment ✅ ❌ ❌

---

## 4. Role-Based Authorization

Role-based rules should be implemented with ASP.NET Core authorization
attributes where the rule is simple and does not depend on a specific
resource.

Examples:

```csharp
[Authorize]
```

```csharp
[Authorize(Roles = "Doctor")]
```

```csharp
[Authorize(Roles = "Admin")]
```

Role checks answer the question:

> **What type of user is making this request?**

They do not, by themselves, answer whether the user owns or is allowed
to access a particular resource.

---

## 5. Resource Ownership

Some permissions require more than a role check.

### 5.1 Doctor → Patient

A doctor should not automatically have access to every patient merely
because the user has the `Doctor` role.

The intended rule is:

```text
Doctor
   |
   +-- Assigned Patient A     → Allowed
   |
   +-- Assigned Patient B     → Allowed
   |
   +-- Unrelated Patient C    → Forbidden
```

The API should verify the relationship between the authenticated doctor
and the requested patient/resource.

This rule is expected to apply to patient-related resources such as:

- Patients
- Vital Signs
- Patient Medications
- Appointments

where the underlying data relationship supports the check.

### 5.2 Patient → Own Data

A patient should only access resources belonging to that patient.

For example:

```text
Patient A
   |
   +-- Patient A vital signs       → Allowed
   |
   +-- Patient A medications       → Allowed
   |
   +-- Patient B vital signs       → Forbidden
```

Ownership checks should be performed using the authenticated user's
identity and the resource's patient relationship.

---

## 6. Age Requirement

Age is treated as a **business requirement**, not as a role.

The planned rule is:

> Certain protected operations may require the authenticated user to be
> at least 18 years old.

This should not create an additional role such as `Adult`.

Instead, the authorization model should remain:

```text
Role
 ├── Admin
 ├── Doctor
 └── Patient

Business requirement
 └── Age >= 18
```

This requirement is a candidate for **Policy-Based Authorization**.

Example:

```csharp
[Authorize(Policy = "AdultOnly")]
```

The exact endpoints requiring this policy should be decided when the
corresponding business operation is implemented.

---

## 7. Authorization Levels

The project will use the simplest authorization mechanism that correctly
expresses each rule.

### Level 1 --- Role-Based Authorization

Use when the rule depends only on the user's role.

```csharp
[Authorize(Roles = "Doctor")]
```

### Level 2 --- Policy-Based Authorization

Use when the rule represents a reusable business requirement.

Example:

```text
Age >= 18
```

### Level 3 --- Resource-Based Authorization

Use when authorization depends on the relationship between the
authenticated user and a specific resource.

Examples:

```text
Doctor → assigned Patient
Patient → own data
```

This can be implemented using ASP.NET Core's authorization services and
custom authorization handlers when needed.

---

## 8. HTTP Authorization Outcomes

The API should distinguish authentication failures from authorization
failures.

---

Status Meaning

---

`401 Unauthorized` The request does not contain valid
authentication credentials.

`403 Forbidden` The user is authenticated but does
not have permission to perform the
requested operation.

---

Examples:

```text
No JWT
  → 401 Unauthorized

Valid JWT + wrong role
  → 403 Forbidden

Valid Doctor JWT + unrelated Patient resource
  → 403 Forbidden
```

---

## 9. Implementation Strategy

Authorization should be implemented incrementally.

### Completed

- ASP.NET Core Identity
- User registration
- JWT authentication
- JWT validation
- Role claims in JWT
- `Admin`, `Doctor`, and `Patient` roles
- Role-based authorization foundation
- Authorization middleware

### Next

1.  Apply role rules to the remaining controllers.
2.  Add and test resource ownership rules.
3.  Introduce the age requirement where a real business operation
    requires it.
4.  Add policy-based authorization for reusable requirements.
5.  Add resource-based authorization handlers for ownership checks.
6.  Test `401` and `403` scenarios through Postman/Swagger.

---

## 10. Testing Requirements

Each authorization rule should be tested with at least:

- Valid authorized user → expected success.
- Unauthenticated request → `401 Unauthorized`.
- Authenticated user with insufficient role → `403 Forbidden`.
- Authenticated user with the correct role but without resource
  ownership → `403 Forbidden`.
- Valid owner/authorized user → expected success.

Authorization tests should be documented alongside the relevant
API/Postman tests.

---

## 11. Future Authorization Rules

The matrix is intentionally extensible.

Future rules may include:

- Doctor-specific patient assignments.
- Patient ownership of health records.
- Appointment ownership/assignment.
- Additional business requirements expressed as policies.
- More granular permissions if the system grows beyond the current
  three-role model.

New rules should be documented here before implementation whenever
possible.
