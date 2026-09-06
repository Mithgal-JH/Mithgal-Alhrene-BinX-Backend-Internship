# Week 8 — Sprint 3: Advanced Queries, Caching & Performance

## Week Overview

Week 8 begins **Sprint 3** of the Cardiac Patient Monitoring System.

The sprint focuses on improving the existing API through advanced querying with LINQ, caching using Redis, and performance optimization.

**Status:** 🟡 In Progress  
**Current Day:** Day 1 — Completed  
**Week Status:** In Progress

## Week Objectives

During this week, the main goals are to:

- Implement advanced LINQ-based queries.
- Improve API data retrieval and response shaping.
- Introduce caching using Redis.
- Reduce unnecessary database work.
- Improve API performance.
- Measure and compare request execution times.
- Apply performance-oriented backend practices to the Cardiac Patient Monitoring System.

## Daily Progress

| Day | Topic | Status |
|---|---|---|
| Day 1 | Advanced Queries & Appointment Summary | ✅ Completed |
| Day 2 | Redis Caching | ⏳ Not Started |
| Day 3 | Cache Integration & Invalidation | ⏳ Not Started |
| Day 4 | Performance Tuning & Optimization | ⏳ Not Started |
| Day 5 | Sprint Review, Testing & Retrospective | ⏳ Not Started |

---

## Day 1 — Completed

### Advanced Appointment Queries & Summary Endpoint

Day 1 focused on extending the existing **Appointments API** with an aggregated summary endpoint.

The goal was to provide a simplified response containing the important appointment information together with the related patient and doctor names.

### New Endpoint

```http
GET /api/appointments/summary
```

### Appointment Summary DTO

The endpoint returns an `AppointmentSummaryDto` containing:

- `AppointmentId`
- `AppointmentDate`
- `AppointmentType`
- `Status`
- `PatientName`
- `DoctorName`

This keeps the response focused on the information required by the summary instead of exposing the complete database entities.

### Service Implementation

The summary functionality was implemented inside:

```text
Services/
└── AppointmentService.cs
```

A new asynchronous method was added:

```csharp
public async Task<IEnumerable<AppointmentSummaryDto>> GetSummaryAsync()
```

The method first retrieves the appointments using Entity Framework Core:

```csharp
var appointments = await _context.Appointments
    .AsNoTracking()
    .ToListAsync();
```

`AsNoTracking()` is used because the summary operation only reads data and does not require EF Core change tracking.

### Related Data

For each appointment, the service retrieves the related:

- Patient
- Doctor

and maps the required fields into an `AppointmentSummaryDto`.

Patient and doctor names are constructed from their first and last names:

```csharp
PatientName = patient is null
    ? "Unknown"
    : $"{patient.FirstName} {patient.LastName}",

DoctorName = doctor is null
    ? "Unknown"
    : $"{doctor.FirstName} {doctor.LastName}"
```

This also provides a fallback value when a related record cannot be found.

### Controller Integration

The endpoint is exposed through:

```text
Controllers/
└── AppointmentsController.cs
```

The controller calls the appointment service and returns the generated summary:

```http
GET /api/appointments/summary
```

Successful requests return:

```text
200 OK
```

with the appointment summary collection.

---

## API Testing

The endpoint was tested using **Postman**.

### Test Request

```http
GET /api/appointments/summary
```

### Result

```text
200 OK
```

The response successfully returned appointment summary objects containing:

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

Additional appointment records were also returned successfully.

### Database Verification

The application successfully executed the required EF Core queries against the database.

The application log confirmed queries against:

```text
"Appointments"
"Patients"
"Doctors"
```

The request completed successfully with:

```text
HTTP GET /api/appointments/summary responded 200
```

This confirms that the endpoint is correctly connected to the existing database and service layer.

---

## Application Verification

The API was successfully started using:

```bash
dotnet run
```

The application started successfully on:

```text
http://localhost:5180
```

The login endpoint was also verified successfully:

```http
POST /api/auth/login
```

Result:

```text
200 OK
```

This confirmed that the existing authentication system continued to work correctly after the Day 1 changes.

---

## Performance & Query Considerations

The summary implementation uses:

```csharp
AsNoTracking()
```

for read-only database operations.

This avoids unnecessary Entity Framework Core change tracking and is appropriate for query endpoints where returned entities are not modified.

The endpoint also demonstrates how the service layer can transform database entities into a purpose-specific DTO before returning the response to the client.

Further query optimization and caching will be addressed during the remaining Sprint 3 days.

---

## Day 1 Outcome

- [x] Sprint 3 Day 1 completed
- [x] Appointment summary functionality implemented
- [x] `AppointmentSummaryDto` used for response shaping
- [x] `GetSummaryAsync()` added to `AppointmentService`
- [x] `GET /api/appointments/summary` implemented
- [x] `AsNoTracking()` applied to read-only appointment queries
- [x] Patient and doctor information included in the summary
- [x] API tested successfully in Postman
- [x] Endpoint returned `200 OK`
- [x] Database queries executed successfully
- [x] Existing authentication verified successfully
- [x] Application runs successfully with `dotnet run`

---

## Evidence

Day 1 evidence includes:

- Appointment service implementation.
- Appointment summary DTO.
- Appointments controller endpoint.
- Successful `dotnet run` output.
- EF Core database query logs.
- Postman request and `200 OK` response for:
  `GET /api/appointments/summary`

---

## Next

Day 2 will continue Sprint 3 with **Redis caching** and integrating caching into the API to reduce repeated database queries and improve response performance.

---

## Sprint 3 Progress

```text
Day 1  → ✅ Completed
Day 2  → ⏳ Not Started
Day 3  → ⏳ Not Started
Day 4  → ⏳ Not Started
Day 5  → ⏳ Not Started
```

**Sprint 3 — In Progress 🟡**
