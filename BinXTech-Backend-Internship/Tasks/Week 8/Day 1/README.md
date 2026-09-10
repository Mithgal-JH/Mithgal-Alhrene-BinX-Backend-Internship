# Week 8 — Day 1
## Sprint 3 — Advanced Queries & Appointment Summary

### Day Overview

Day 1 of Sprint 3 focused on improving the **Cardiac Patient Monitoring System** by adding an appointment summary endpoint.

The goal was to return a simplified view of appointments containing the essential appointment information together with the related patient and doctor names.

---

## Objectives

- Add an appointment summary operation.
- Create a dedicated DTO for the summary response.
- Retrieve appointment data asynchronously using Entity Framework Core.
- Use `AsNoTracking()` for read-only queries.
- Include related patient and doctor information.
- Expose the functionality through the Appointments API.
- Test the endpoint using Postman.
- Verify successful database execution and API response.

---

## Implementation

### 1. Appointment Summary DTO

A dedicated `AppointmentSummaryDto` was used to shape the API response.

The summary contains:

- `AppointmentId`
- `AppointmentDate`
- `AppointmentType`
- `Status`
- `PatientName`
- `DoctorName`

This prevents the endpoint from returning the complete database entities and keeps the response focused on the required information.

---

### 2. Appointment Service

The summary logic was implemented in:

```text
Services/
└── AppointmentService.cs
```

A new asynchronous method was added:

```csharp
public async Task<IEnumerable<AppointmentSummaryDto>> GetSummaryAsync()
```

Appointments are retrieved using:

```csharp
var appointments = await _context.Appointments
    .AsNoTracking()
    .ToListAsync();
```

`AsNoTracking()` is appropriate here because the endpoint only reads data and does not need Entity Framework Core change tracking.

---

### 3. Patient & Doctor Information

For every appointment, the service retrieves the related patient and doctor.

The response maps their names using their first and last names:

```csharp
PatientName = patient is null
    ? "Unknown"
    : $"{patient.FirstName} {patient.LastName}",

DoctorName = doctor is null
    ? "Unknown"
    : $"{doctor.FirstName} {doctor.LastName}"
```

The `"Unknown"` fallback prevents the summary operation from failing if a related record is unavailable.

---

### 4. Controller Endpoint

The functionality was exposed through:

```text
Controllers/
└── AppointmentsController.cs
```

New endpoint:

```http
GET /api/appointments/summary
```

A successful request returns:

```text
200 OK
```

---

## Testing with Postman

The endpoint was tested successfully in Postman.

### Request

```http
GET {{baseUrl}}/appointments/summary
```

### Response

```text
200 OK
```

The response returned appointment summary records including appointment details, patient names, and doctor names.

Example:

```json
{
  "appointmentId": 2,
  "appointmentDate": "2026-08-15T10:00:00Z",
  "appointmentType": "Checkup",
  "status": "Scheduled",
  "patientName": "Ahmad Updated Ali",
  "doctorName": "Ahmad Updated Ali"
}
```

Additional appointment records were returned successfully.

---

## Database Verification

The application logs confirmed that Entity Framework Core successfully queried the required tables.

The executed queries included:

```text
"Appointments"
"Patients"
"Doctors"
```

The logs confirmed successful execution of the appointment query followed by the related patient and doctor lookups.

The final request timing log showed:

```text
HTTP GET /api/appointments/summary responded 200
```

This confirms that the endpoint successfully communicates with the database and returns the expected result.

---

## Application Run

The application was successfully started using:

```bash
dotnet run
```

Build result:

```text
CardiacPatientMonitoringSystem net10.0 succeeded
```

The API started successfully at:

```text
http://localhost:5180
```

Authentication was also verified during testing:

```http
POST /api/auth/login
```

Result:

```text
200 OK
```

---

## Request Timing

The existing `RequestTimingMiddleware` was used to observe endpoint execution time.

The successful summary request was logged as:

```text
HTTP GET /api/appointments/summary responded 200 in 204 ms
```

This provides a baseline that can be used for later performance and caching improvements during Sprint 3.

---

## Issues / Warnings Observed

The application runs successfully, but the console reports existing warnings:

### Microsoft.OpenApi vulnerability warning

```text
NU1903: Package 'Microsoft.OpenApi' 2.0.0 has a known high severity vulnerability
```

This is a dependency warning and did not prevent the application from building or running.

### EF Core Sensitive Data Logging

```text
Sensitive data logging is enabled.
```

This is enabled for the development environment and should not be enabled in production.

### HTTPS Redirection Warning

```text
Failed to determine the https port for redirect.
```

The API still runs successfully over:

```text
http://localhost:5180
```

---

## Day 1 Outcome

- [x] `AppointmentSummaryDto` implemented
- [x] `GetSummaryAsync()` implemented
- [x] `GET /api/appointments/summary` added
- [x] Read-only queries use `AsNoTracking()`
- [x] Patient names included in the summary
- [x] Doctor names included in the summary
- [x] API tested through Postman
- [x] `200 OK` response confirmed
- [x] EF Core database queries verified
- [x] Existing authentication verified
- [x] Request timing verified
- [x] Application builds and runs successfully

---

## Evidence

The following evidence was captured for Day 1:

1. `AppointmentService.cs` showing `GetSummaryAsync()`.
2. Postman request for `GET /api/appointments/summary`.
3. Postman `200 OK` response with appointment summary data.
4. Terminal output showing successful `dotnet run`.
5. EF Core logs showing queries against Appointments, Patients, and Doctors.
6. Request timing log showing the successful `200` response.

---

## Next Step

Day 2 will continue Sprint 3 by working on **Redis caching**, with the goal of reducing repeated database queries and improving API response performance.

---

### Day 1 Status

**✅ Completed**

**Sprint 3 — In Progress**
