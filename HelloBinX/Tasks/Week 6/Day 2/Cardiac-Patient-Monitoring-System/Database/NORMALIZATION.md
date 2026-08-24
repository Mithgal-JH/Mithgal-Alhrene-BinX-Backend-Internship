# Cardiac Patient Monitoring System

## Database Normalization

This document describes the database design from the initial business-oriented model through the normalized final design.

---

## 1. Initial Database Design

The initial design contains the following main entities:

- `Patients`
- `Doctors`
- `VitalSigns`
- `Medications`
- `Appointments`
- `Users`

### Initial Relationships

- One Patient can have many Vital Signs records.
- One Patient can have many Appointments.
- One Doctor can have many Appointments.
- Patients and Medications initially have a many-to-many relationship.

---

## 2. First Normal Form (1NF)

The design follows 1NF by keeping attributes atomic and avoiding repeating groups.

Examples:

- Patient information is stored as individual attributes.
- Vital-sign measurements are stored as separate records rather than repeated groups inside a Patient record.
- Each appointment is stored as its own record.

At this stage, the Patient–Medication relationship is still considered as a many-to-many relationship.

---

## 3. Second Normal Form (2NF)

2NF removes partial dependencies on part of a composite key.

The main entities use single-column primary keys, so there are no partial-key dependencies in those tables.

The Patient–Medication many-to-many relationship requires an associative entity because some attributes describe the relationship between a patient and a medication rather than the medication itself.

### PatientMedications

The relationship is resolved using:

- `PatientMedicationId`
- `PatientId`
- `MedicationId`
- `Dosage`
- `Frequency`
- `Route`
- `StartDate`
- `EndDate`
- `Status`
- `Notes`

This allows medication information to remain in `Medications` while patient-specific treatment information is stored in `PatientMedications`.

---

## 4. Third Normal Form (3NF)

3NF removes transitive dependencies so that non-key attributes depend on the key and not on another non-key attribute.

The final design separates responsibilities between entities:

- Doctor information belongs to `Doctors`.
- Medication catalog information belongs to `Medications`.
- Patient-specific medication information belongs to `PatientMedications`.
- Appointment-specific information belongs to `Appointments`.
- Vital-sign measurements belong to `VitalSigns`.
- Patient information belongs to `Patients`.

This reduces unnecessary duplication and keeps the database structure consistent.

---

## 5. Final Normalized Tables

### Patients

Stores patient demographic, contact, emergency-contact, and medical information.

**Primary Key:** `PatientId`

### Doctors

Stores doctor identity, contact information, specialization, and license information.

**Primary Key:** `DoctorId`

### VitalSigns

Stores time-based vital-sign measurements for patients.

**Primary Key:** `VitalSignId`

**Foreign Key:** `PatientId → Patients`

### Medications

Stores medication catalog information.

**Primary Key:** `MedicationId`

### PatientMedications

Stores the relationship between patients and medications, including patient-specific treatment information.

**Primary Key:** `PatientMedicationId`

**Foreign Keys:**

- `PatientId → Patients`
- `MedicationId → Medications`

### Appointments

Stores appointments between patients and doctors.

**Primary Key:** `AppointmentId`

**Foreign Keys:**

- `PatientId → Patients`
- `DoctorId → Doctors`

### Users

`Users` represents the business concept of system users.

For the actual implementation, user authentication and roles will be handled through ASP.NET Core Identity rather than a custom password table.

---

## 6. Final Relationships

```text
Patients 1 ──────── * VitalSigns

Patients 1 ──────── * Appointments * ──────── 1 Doctors

Patients 1 ──────── * PatientMedications * ──────── 1 Medications
```

Therefore:

- `Patients → VitalSigns` = One-to-Many
- `Patients → Appointments` = One-to-Many
- `Doctors → Appointments` = One-to-Many
- `Patients → PatientMedications` = One-to-Many
- `Medications → PatientMedications` = One-to-Many
- `Patients ↔ Medications` = Many-to-Many resolved through `PatientMedications`

---

## 7. Final Design Summary

The normalized database consists of:

```text
Patients
Doctors
VitalSigns
Medications
PatientMedications
Appointments
```

Authentication and authorization users/roles will be provided by ASP.NET Core Identity.

The final relational design will be implemented using Entity Framework Core, relationships, and migrations.
