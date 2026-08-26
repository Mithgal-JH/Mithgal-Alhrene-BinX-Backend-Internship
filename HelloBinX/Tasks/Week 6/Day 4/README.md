# Week 6 — Day 4
## Database Transactions & Rollback Testing

### Objective

The goal of Day 4 was to implement and test a real multi-step write operation using a database transaction.

The selected operation was creating an Appointment together with Notifications for both the Patient and the Doctor.

---

## Implemented Feature

When a new Appointment is created, the system performs the following operations:

1. Validate the Patient
2. Validate the Doctor
3. Validate ownership/authorization
4. Create the Appointment
5. Create a Notification for the Patient
6. Create a Notification for the Doctor
7. Commit all changes together

All database write operations are wrapped inside a single transaction.

---

## Transaction Flow

```text
Begin Transaction
        ↓
Create Appointment
        ↓
Save Appointment
        ↓
Create Patient Notification
        ↓
Create Doctor Notification
        ↓
Save Notifications
        ↓
Commit Transaction
```

If any operation fails:

```text
Begin Transaction
        ↓
Create Appointment
        ↓
Create Notifications
        ↓
Exception
        ↓
Rollback
        ↓
All changes are removed
```

---

## Notifications

A new `Notifications` table was added to store appointment-related notifications.

Each notification contains:

- `NotificationId`
- `UserId`
- `AppointmentId`
- `Message`
- `CreatedAt`
- `IsRead`

Notifications are created for:

- Patient
- Doctor

The Notification is also linked to the Appointment.

---

## Doctor User Account

The Doctor creation flow was updated so that a Doctor created by an Admin is also linked to an Identity User.

The process is:

```text
Admin
  ↓
Create Doctor
  ↓
Create Identity User
  ↓
Assign Doctor Role
  ↓
Create Doctor Record
  ↓
Doctor.UserId = User.Id
```

The User and Doctor creation are also handled inside a transaction to prevent partially created records.

---

## Migration

A new EF Core migration was created for Notifications:

```text
AddNotifications
```

The migration was successfully applied using:

```bash
dotnet ef migrations add AddNotifications
dotnet ef database update
```

---

## Transaction Testing

Two scenarios were tested.

### 1. Successful Transaction

A valid Create Appointment request was sent.

Expected result:

```text
201 Created
```

The database contained:

- The new Appointment
- Patient Notification
- Doctor Notification

This confirmed that the transaction was successfully committed.

---

### 2. Rollback Test

To test rollback behavior, an intentional exception was temporarily added before `CommitAsync()`:

```csharp
throw new Exception("Testing transaction rollback");
```

The request returned:

```text
500 Internal Server Error
```

The application logs confirmed that the exception occurred after the Appointment and Notifications had been inserted.

The database was then checked.

The Appointment created during the failed transaction was not persisted, and the related Notifications were also not persisted.

This confirmed that the transaction successfully rolled back all changes.

---

## Final Successful Test

After completing the rollback test, the temporary exception was removed.

The same request was executed again and returned:

```text
201 Created
```

The Appointment and both Notifications were successfully stored in the database.

---

## Test Scenario

The complete tested flow was:

```text
Before Test
    ↓
Create Appointment
    ↓
Create Notifications
    ↓
Intentional Exception
    ↓
HTTP 500
    ↓
Transaction Rollback
    ↓
Verify Appointment Removed
    ↓
Verify Notifications Removed
    ↓
Remove Test Exception
    ↓
Retry Request
    ↓
HTTP 201 Created
    ↓
Verify Appointment Saved
    ↓
Verify Notifications Saved
```

---

## Documentation

The complete transaction and rollback test was documented with screenshots.

Documentation:

`Week_6_Day_4_Transaction_Rollback_Documentation_EN_Corrected.pdf`

The documentation includes:

- Database state before the test
- Failed Postman request
- Exception and application logs
- Database state after rollback
- Successful retry
- Final Appointment and Notification records

---

## Key Concepts Practiced

- EF Core Transactions
- `BeginTransactionAsync()`
- `CommitAsync()`
- `RollbackAsync()`
- Atomic database operations
- Multi-step write operations
- Notification creation
- Identity User and Doctor relationship
- Transaction failure testing
- Database consistency
- Postman API testing
- PostgreSQL verification

---

## Result

Day 4 successfully implemented and verified a real transactional workflow.

The system now guarantees that creating an Appointment and its related Notifications behaves as one atomic operation:

> Either all changes are committed, or all changes are rolled back.
