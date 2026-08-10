# Cardiac Patient Monitoring System — API Documentation

## Base URL

```text
http://localhost:5180/api
```

In Postman, the collection uses:

```text
{{baseUrl}}
```

## Overview

The API provides CRUD operations for:

- Patients
- Doctors
- Medications
- Patient Medications
- Appointments
- Vital Signs

All request and response DTOs are JSON-based.

---

## 1. Patients

### Get All Patients

```http
GET {{baseUrl}}/patients
```

Returns all patients.

**Success:** `200 OK`

### Get Patient By ID

```http
GET {{baseUrl}}/patients/{id}
```

**Success:** `200 OK`  
**Not found:** `404 Not Found`

### Create Patient

```http
POST {{baseUrl}}/patients
```

Example request:

```json
{
  "medicalRecordNumber": "MRN-0002",
  "firstName": "Khaled",
  "lastName": "Ahmad",
  "dateOfBirth": "1988-10-20",
  "gender": "Male",
  "phone": "0599444444",
  "email": "khaled.ahmad@example.com",
  "address": "Hebron, Palestine",
  "emergencyContactName": "Ali Ahmad",
  "emergencyContactPhone": "0599555555",
  "medicalNotes": "No known allergies"
}
```

**Success:** `201 Created`

### Update Patient

```http
PUT {{baseUrl}}/patients/{id}
```

**Success:** `200 OK`  
**Not found:** `404 Not Found`

### Delete Patient

```http
DELETE {{baseUrl}}/patients/{id}
```

**Success:** `204 No Content`  
**Not found:** `404 Not Found`

---

## 2. Doctors

### Get All Doctors

```http
GET {{baseUrl}}/doctors
```

**Success:** `200 OK`

### Get Doctor By ID

```http
GET {{baseUrl}}/doctors/{id}
```

**Success:** `200 OK`  
**Not found:** `404 Not Found`

### Create Doctor

```http
POST {{baseUrl}}/doctors
```

Example request:

```json
{
  "firstName": "Omar",
  "lastName": "Hassan",
  "email": "omar.hassan@example.com",
  "phone": "0599222222",
  "specialization": "Cardiology",
  "licenseNumber": "LIC-0001"
}
```

**Success:** `201 Created`  
**Duplicate license number:** `409 Conflict`

### Update Doctor

```http
PUT {{baseUrl}}/doctors/{id}
```

**Success:** `200 OK`  
**Not found:** `404 Not Found`  
**Duplicate license number:** `409 Conflict`

### Delete Doctor

```http
DELETE {{baseUrl}}/doctors/{id}
```

**Success:** `204 No Content`  
**Not found:** `404 Not Found`

---

## 3. Medications

### Get All Medications

```http
GET {{baseUrl}}/medications
```

**Success:** `200 OK`

### Get Medication By ID

```http
GET {{baseUrl}}/medications/{id}
```

**Success:** `200 OK`  
**Not found:** `404 Not Found`

### Create Medication

```http
POST {{baseUrl}}/medications
```

Example request:

```json
{
  "name": "Aspirin",
  "genericName": "Acetylsalicylic Acid",
  "description": "Used as an antiplatelet medication.",
  "strength": "100 mg",
  "dosageForm": "Tablet",
  "manufacturer": "Bayer"
}
```

**Success:** `201 Created`

### Update Medication

```http
PUT {{baseUrl}}/medications/{id}
```

**Success:** `200 OK`  
**Not found:** `404 Not Found`

### Delete Medication

```http
DELETE {{baseUrl}}/medications/{id}
```

**Success:** `204 No Content`  
**Not found:** `404 Not Found`

---

## 4. Patient Medications

Patient Medications represent the relationship between a patient and a medication, including dosage and treatment details.

### Get All Patient Medications

```http
GET {{baseUrl}}/patientmedications
```

**Success:** `200 OK`

### Get Patient Medication By ID

```http
GET {{baseUrl}}/patientmedications/{id}
```

**Success:** `200 OK`  
**Not found:** `404 Not Found`

### Create Patient Medication

```http
POST {{baseUrl}}/patientmedications
```

Example request:

```json
{
  "patientId": 2,
  "medicationId": 2,
  "dosage": "50 mg",
  "frequency": "Twice daily",
  "route": "Oral",
  "startDate": "2026-08-10",
  "endDate": null,
  "status": "Active",
  "notes": "Take after meals"
}
```

`PatientId` and `MedicationId` must reference existing records.

**Success:** `201 Created`  
**Invalid patient or medication:** `400 Bad Request`

### Update Patient Medication

```http
PUT {{baseUrl}}/patientmedications/{id}
```

**Success:** `200 OK`  
**Not found:** `404 Not Found`

### Delete Patient Medication

```http
DELETE {{baseUrl}}/patientmedications/{id}
```

**Success:** `204 No Content`  
**Not found:** `404 Not Found`

---

## 5. Appointments

Appointments connect an existing patient with an existing doctor.

### Get All Appointments

```http
GET {{baseUrl}}/appointments
```

**Success:** `200 OK`

### Get Appointment By ID

```http
GET {{baseUrl}}/appointments/{id}
```

**Success:** `200 OK`  
**Not found:** `404 Not Found`

### Create Appointment

```http
POST {{baseUrl}}/appointments
```

Example request:

```json
{
  "patientId": 2,
  "doctorId": 2,
  "appointmentDate": "2026-08-15T10:00:00Z",
  "appointmentType": "Cardiology Consultation",
  "status": "Scheduled",
  "reason": "Routine cardiac follow-up",
  "notes": "Bring previous medical reports"
}
```

`PatientId` and `DoctorId` must reference existing records.

**Success:** `201 Created`  
**Invalid patient or doctor:** `400 Bad Request`

### Update Appointment

```http
PUT {{baseUrl}}/appointments/{id}
```

**Success:** `200 OK`  
**Not found:** `404 Not Found`

### Delete Appointment

```http
DELETE {{baseUrl}}/appointments/{id}
```

**Success:** `204 No Content`  
**Not found:** `404 Not Found`

---

## 6. Vital Signs

Vital Signs are recorded measurements associated with a patient.

There is no update endpoint for Vital Signs in the current API design. A new measurement should be recorded instead of modifying an existing historical measurement.

### Get All Vital Signs

```http
GET {{baseUrl}}/vitalsigns
```

**Success:** `200 OK`

### Get Vital Sign By ID

```http
GET {{baseUrl}}/vitalsigns/{id}
```

**Success:** `200 OK`  
**Not found:** `404 Not Found`

### Create Vital Sign

```http
POST {{baseUrl}}/vitalsigns
```

Example request:

```json
{
  "patientId": 2,
  "heartRate": 72,
  "systolicBloodPressure": 120,
  "diastolicBloodPressure": 80,
  "respiratoryRate": 16,
  "temperature": 36.8,
  "oxygenSaturation": 98,
  "weight": 75.5,
  "recordedAt": "2026-08-10T17:30:00Z",
  "notes": "Normal vital signs"
}
```

`PatientId` must reference an existing patient.

**Success:** `201 Created`  
**Invalid patient:** `400 Bad Request`

### Delete Vital Sign

```http
DELETE {{baseUrl}}/vitalsigns/{id}
```

**Success:** `204 No Content`  
**Not found:** `404 Not Found`

---

## Endpoint Summary

| Resource | GET All | GET By ID | POST | PUT | DELETE |
|---|---:|---:|---:|---:|---:|
| Patients | ✓ | ✓ | ✓ | ✓ | ✓ |
| Doctors | ✓ | ✓ | ✓ | ✓ | ✓ |
| Medications | ✓ | ✓ | ✓ | ✓ | ✓ |
| Patient Medications | ✓ | ✓ | ✓ | ✓ | ✓ |
| Appointments | ✓ | ✓ | ✓ | ✓ | ✓ |
| Vital Signs | ✓ | ✓ | ✓ | — | ✓ |

## Postman

The accompanying Postman collection is organized into folders by resource and uses the `baseUrl` collection variable.

Import:

```text
Cardiac-Patient-Monitoring-System.postman_collection.json
```

Then set `baseUrl` to the URL of the running API if the local port changes.
