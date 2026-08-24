# Week 6 — Day 2
## EF Core Data Model, Fluent API & Database Seeding

### Overview

Day 2 focused on reviewing and strengthening the Entity Framework Core data model for the Cardiac Patient Monitoring System.

The main work included configuring entity relationships using Fluent API, defining delete behaviors and indexes, adding domain seed data, creating and reviewing an EF Core migration, applying the migration to PostgreSQL, and verifying the resulting database state.

---

## Objectives

- Review the existing EF Core entities.
- Configure entity relationships using Fluent API.
- Define explicit `DeleteBehavior` rules.
- Configure required fields, maximum lengths, precision, and indexes.
- Add reference/domain seed data using `HasData`.
- Create an EF Core migration for the seed data.
- Review the generated migration before applying it.
- Apply the migration to PostgreSQL.
- Verify the seeded records and migration history.
- Run the existing automated test suite.

---

## 1. EF Core Data Model

The system uses the following main entities:

- `Patient`
- `Doctor`
- `VitalSign`
- `Medication`
- `PatientMedication`
- `Appointment`

The main relationships are:

```text
Patient
 ├── VitalSigns
 ├── Appointments ─── Doctor
 └── PatientMedications ─── Medication
```

---

## 2. Fluent API Configuration

Entity configurations are defined in:

```text
Data/ApplicationDbContext.cs
```

The `ApplicationDbContext` configures:

- Primary keys
- Required properties
- Maximum string lengths
- Decimal precision
- Unique indexes
- Foreign key relationships
- Delete behaviors

### Delete Behavior

The application uses:

```csharp
DeleteBehavior.Restrict
```

for the main domain relationships.

This prevents accidental cascading deletion of important patient-related medical data.

---

## 3. Indexes

Unique indexes were configured for important identifiers:

```text
Patients.MedicalRecordNumber
Patients.UserId
Doctors.LicenseNumber
Doctors.UserId
```

These indexes help enforce data uniqueness and support efficient lookups.

---

## 4. Medication Seed Data

Reference data was added using EF Core `HasData`.

The seeded medications are:

| ID | Name | Generic Name | Strength |
|---:|---|---|---|
| 101 | Aspirin | Acetylsalicylic Acid | 81 mg |
| 102 | Atorvastatin | Atorvastatin | 20 mg |

An existing `Metoprolol` record with `MedicationId = 2` was already present in the database, so it was not inserted again.

---

## 5. EF Core Migration

A new migration was created:

```text
20260824164822_AddMedicationSeedData
```

Command used:

```powershell
dotnet ef migrations add AddMedicationSeedData
```

The generated migration inserts the two new medication records and removes them in its `Down()` method.

The migration was reviewed before applying it.

---

## 6. Database Update

The migration was successfully applied using:

```powershell
dotnet ef database update
```

The migration was recorded in:

```text
__EFMigrationsHistory
```

The current migration history includes:

```text
20260810131740_InitialCreate
20260810212953_AddIdentity
20260813120654_AddUserIdToPatientAndDoctor
20260824164822_AddMedicationSeedData
```

---

## 7. PostgreSQL Verification

The `Medications` table was verified through pgAdmin.

The final data includes:

```text
2    Metoprolol
101  Aspirin
102  Atorvastatin
```

This confirmed that the new seed data was successfully inserted without affecting the existing medication record.

---

## 8. Testing

The complete automated test suite was executed using:

```powershell
dotnet test
```

### Test Results

```text
Total:   15
Passed:  15
Failed:  0
Skipped: 0
```

All tests passed successfully.

---

## 9. Commands Used

```powershell
dotnet build

dotnet ef migrations add AddMedicationSeedData

dotnet ef migrations list

dotnet ef migrations remove

dotnet ef database update

dotnet test

git status
```

---

## 10. Outcome

Day 2 successfully strengthened the EF Core database layer by:

- Configuring the domain model with Fluent API.
- Defining explicit relationships and delete behaviors.
- Maintaining unique database constraints.
- Adding reference seed data.
- Creating and reviewing an EF Core migration.
- Applying the migration successfully to PostgreSQL.
- Verifying the database state through pgAdmin.
- Confirming that all automated tests pass.

### Final Status

**Day 2 — Completed ✅**

**Tests: 15/15 Passed**
