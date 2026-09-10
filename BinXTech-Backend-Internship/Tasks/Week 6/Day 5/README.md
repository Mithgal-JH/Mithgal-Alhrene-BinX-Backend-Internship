# Week 6 — Day 5

## Sprint Review, Testing & Retrospective

Day 5 was the closing day of Sprint 1. The focus was to verify the completed work, run the final test suite, confirm the project builds successfully, and review the sprint outcome.

## Completed Work

- Reviewed the Sprint 1 deliverables.
- Verified the implemented API read operations.
- Reviewed the transaction and rollback implementation from Day 4.
- Ran the complete automated test suite.
- Verified the project build.
- Confirmed the final API behavior through the existing Postman evidence.
- Completed the Sprint 1 review and retrospective.

## Final Test Result

```text
Total:   15
Passed:  15
Failed:  0
Skipped: 0
```

All 15 automated tests passed successfully.

Command used:

```bash
dotnet test
```

## Build Result

```text
Build succeeded
```

The project built successfully. A package vulnerability warning for `Microsoft.OpenApi 2.0.0` was reported, but it did not cause test or build failure.

## Sprint 1 Review

The sprint successfully covered:

```text
EF Core & Database
       ↓
Pagination
       ↓
Search & Filtering
       ↓
Sorting
       ↓
DTO Projection
       ↓
Appointment Creation
       ↓
Notifications
       ↓
Transactions
       ↓
Rollback Handling
       ↓
Automated Testing
```

## Retrospective

### What Went Well

- The project was developed incrementally throughout the sprint.
- Read operations became more flexible through pagination, filtering, and sorting.
- DTO projection kept API responses separated from database entities.
- Transaction handling protected multi-step appointment operations.
- Database behavior was verified directly through PostgreSQL.
- The final automated test suite passed completely.

### What Could Be Improved

- Testing can be planned alongside implementation instead of being left until the end of a task.
- Documentation and implementation should continue to be updated together throughout the sprint.

### Action for Sprint 2

Focus on writing tests alongside important business logic and continue keeping API documentation synchronized with implementation changes.

## Final Status

```text
Day 5 → ✅ Completed
Sprint 1 → ✅ Completed
Week 6 → ✅ Completed
```

**Sprint 1 successfully completed.**
