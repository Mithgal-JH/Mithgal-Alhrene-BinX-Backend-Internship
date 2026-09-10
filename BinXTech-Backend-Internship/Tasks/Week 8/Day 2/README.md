# Week 8 — Day 2: Query Optimization with Eager & Explicit Loading

## Overview

Day 2 focused on identifying and fixing an **N+1 query problem** in the Cardiac Patient Monitoring System and measuring the improvement.

The work covered:

- Measuring the existing N+1 behavior.
- Fixing the issue using **Eager Loading** with `Include`.
- Measuring the result after optimization.
- Converting the same list-style endpoint to **Projection** using `Select`.
- Comparing `Include` and Projection.
- Reviewing `AsSplitQuery` applicability.

---

## 1. Before Optimization — N+1 Problem

The original `GetSummaryAsync()` implementation first loaded all appointments:

```csharp
var appointments = await _context.Appointments
    .AsNoTracking()
    .ToListAsync();
```

Then, inside the `foreach`, it executed a separate query for the patient and doctor of every appointment:

```csharp
foreach (var appointment in appointments)
{
    var patient = await _context.Patients
        .AsNoTracking()
        .FirstOrDefaultAsync(p =>
            p.PatientId == appointment.PatientId);

    var doctor = await _context.Doctors
        .AsNoTracking()
        .FirstOrDefaultAsync(d =>
            d.DoctorId == appointment.DoctorId);

    // ...
}
```

With 6 appointments, the observed query pattern was:

```text
1 Appointments query
6 Patient queries
6 Doctor queries

Total = 13 SQL queries
```

The general pattern was:

```text
1 + 2N queries
```

This demonstrated the N+1 problem caused by querying related entities inside the loop.

---

## 2. Fixing N+1 with Eager Loading

The first optimization used Entity Framework Core's **Eager Loading**:

```csharp
var appointments = await _context.Appointments
    .Include(a => a.Doctor)
    .Include(a => a.Patient)
    .AsNoTracking()
    .ToListAsync();
```

The `foreach` no longer performed database queries:

```csharp
foreach (var appointment in appointments)
{
    var patient = appointment.Patient;
    var doctor = appointment.Doctor;

    // ...
}
```

EF Core generated a single SQL query using joins between:

- `Appointments`
- `Patients`
- `Doctors`

### Result

```text
Before: 13 SQL queries
After:   1 SQL query
```

This eliminated the per-appointment database round-trips.

---

## 3. Projection with Select

The same endpoint was then converted to use **Projection**.

Instead of loading complete `Appointment`, `Patient`, and `Doctor` entities, the query directly created the required DTO:

```csharp
return await _context.Appointments
    .AsNoTracking()
    .Select(a => new AppointmentSummaryDto
    {
        AppointmentId = a.AppointmentId,
        AppointmentDate = a.AppointmentDate,
        AppointmentType = a.AppointmentType,
        Status = a.Status,

        PatientName = a.Patient == null
            ? "Unknown"
            : $"{a.Patient.FirstName} {a.Patient.LastName}",

        DoctorName = a.Doctor == null
            ? "Unknown"
            : $"{a.Doctor.FirstName} {a.Doctor.LastName}"
    })
    .ToListAsync();
```

With Projection, `Include` is not required because the navigation properties are referenced directly inside the `Select`.

EF Core generated a single query and selected only the fields required by `AppointmentSummaryDto`.

---

## 4. Include vs Projection

### Eager Loading

`Include` loaded the full related entities.

The generated SQL contained many columns from:

- `Appointments`
- `Doctors`
- `Patients`

### Projection

Projection selected only the required fields:

```text
AppointmentId
AppointmentDate
AppointmentType
Status
Patient FirstName
Patient LastName
Doctor FirstName
Doctor LastName
```

Therefore, Projection was a leaner approach for this list/summary endpoint.

---

## 5. Measurements

### N+1

```text
SQL Queries: 13
```

### Include

```text
SQL Queries: 1
SQL execution time: 11 ms
HTTP request time: 229 ms
```

### Projection

```text
SQL Queries: 1
SQL execution time: 7 ms
HTTP request time: 199 ms
```

The most important improvement was the reduction from **13 queries to 1 query**.

Projection also reduced the amount of data selected compared with the `Include` approach.

> Request timing can vary between runs due to database state, caching, application startup, and other factors. The SQL query shape and query count are the primary measurements for this exercise.

---

## 6. AsSplitQuery

`AsSplitQuery()` is useful when a query includes multiple **collection navigation properties** and a single JOIN-based query could produce a Cartesian explosion.

The current project did not have an applicable endpoint with multiple collection navigation properties for this task.

Therefore:

```text
AsSplitQuery: Not Applicable
```

No artificial implementation was added just to demonstrate the feature.

---

## 7. Final Result

```text
Original N+1
    ↓
13 SQL queries
    ↓
Eager Loading with Include
    ↓
1 SQL query
    ↓
Projection with Select
    ↓
1 SQL query + fewer selected columns
```

### Key Takeaways

- N+1 problems can cause unnecessary database round-trips.
- `Include` can load related entities together.
- Projection is often better for list and summary endpoints when only a subset of fields is required.
- Query count should be measured before and after optimization.
- Optimization should be verified using actual SQL/logging rather than assumed from the code alone.
