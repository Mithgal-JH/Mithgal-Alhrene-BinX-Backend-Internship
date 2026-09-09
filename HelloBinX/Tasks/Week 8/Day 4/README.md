# Day 4 — Database Indexing & Performance Profiling

## Overview

This day focused on improving database query performance through proper indexing and measuring the impact of indexes using PostgreSQL execution plans.

The main goal was to identify frequently filtered/sorted columns, verify existing indexes, use a suitable composite index, and compare query performance before and after index usage.

---

## Objectives

- Understand when database indexes are useful.
- Identify columns frequently used in filtering and sorting.
- Use composite indexes for queries involving multiple columns.
- Analyze query execution using `EXPLAIN (ANALYZE, BUFFERS)`.
- Compare query performance before and after index usage.
- Document measurable performance improvements.

---

## Performance Test Data — Synthetic Seed

To make the performance comparison more meaningful, additional synthetic patient data was inserted into the database.

### Seed Details

- **Entity:** `Patient`
- **Additional records:** 1000 synthetic patients
- **Purpose:** Provide a larger dataset for database performance profiling.
- **Data:** Synthetic/test data only.

The seed data was used only for performance testing and does not represent real patient information.

After running the seed process:

```text
Seeded 1000 performance patients...
1000 performance patients seeded successfully.
```

The API and database were then used to execute the performance queries.

> Synthetic seed data provides reproducible test data without using real patient information.

---

# Index Analysis

## Existing Patient Indexes

The `Patients` table contains the following important indexes:

| Index | Columns | Purpose |
|---|---|---|
| `PK_Patients` | `PatientId` | Primary key lookup |
| `IX_Patients_MedicalRecordNumber` | `MedicalRecordNumber` | Unique medical record lookup |
| `IX_Patients_UserId` | `UserId` | Unique user-to-patient lookup |
| `IX_Patients_Gender_DateOfBirth` | `Gender, DateOfBirth` | Filtering by gender and ordering by date of birth |

The composite index is:

```sql
CREATE INDEX "IX_Patients_Gender_DateOfBirth"
ON public."Patients" USING btree ("Gender", "DateOfBirth");
```

---

# Composite Index Performance Test

The composite index was tested with a query that filters patients by `Gender` and orders the result by `DateOfBirth`.

### Query

```sql
EXPLAIN (ANALYZE, BUFFERS)
SELECT *
FROM "Patients"
WHERE "Gender" = 'Male'
ORDER BY "DateOfBirth"
LIMIT 10 OFFSET 0;
```

---

## Before Index Usage

To compare the query without using an index, index scans were temporarily disabled for the diagnostic test:

```sql
SET enable_indexscan = off;
SET enable_bitmapscan = off;
```

The execution plan used:

```text
Seq Scan on "Patients"
```

Important results:

```text
Rows Removed by Filter: 507
Execution Time: 0.259 ms
```

The database had to scan the table and filter the rows before sorting and returning the requested records.

---

## After Index Usage

Index scans were enabled again and the same query was executed.

The execution plan changed to:

```text
Index Scan using "IX_Patients_Gender_DateOfBirth"
```

Important results:

```text
Index Cond: (Gender = 'Male')
Buffers: shared hit=11
Execution Time: 0.102 ms
```

`Rows Removed by Filter` was no longer reported because the `Gender` condition was handled directly by the index through the `Index Cond`.

---

# Performance Comparison

| Metric | Without Index | With Composite Index |
|---|---:|---:|
| Execution Plan | Sequential Scan | Index Scan |
| Rows Removed by Filter | 507 | Not reported |
| Execution Time | 0.259 ms | 0.102 ms |

### Improvement

Execution time decreased from:

```text
0.259 ms → 0.102 ms
```

Approximate reduction:

```text
60.6%
```

The execution plan also changed from a full sequential table scan to an index scan, demonstrating that the composite index can be used effectively for queries filtering by `Gender` and ordering by `DateOfBirth`.

> The measured timings are from a local PostgreSQL environment and can vary depending on database cache state and system load. The execution-plan change is the main evidence of index usage.

---

# Key Findings

### 1. Indexes should match query patterns

The composite index:

```text
(Gender, DateOfBirth)
```

is useful when the query filters by `Gender` and then works with `DateOfBirth`.

### 2. Column order matters

The index:

```text
(Gender, DateOfBirth)
```

is not equivalent to:

```text
(DateOfBirth, Gender)
```

The leading column affects which queries can efficiently use the index.

### 3. Execution plans provide measurable evidence

`EXPLAIN (ANALYZE, BUFFERS)` was used to compare the query before and after index usage.

The query changed from:

```text
Seq Scan
```

to:

```text
Index Scan
```

with a measured reduction in execution time.

---

# Tools Used

- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- pgAdmin 4
- SQL `EXPLAIN (ANALYZE, BUFFERS)`
- Git & GitHub

---

# Day 4 Outcome

- [x] Identified relevant indexed columns.
- [x] Verified existing indexes.
- [x] Used a composite index on `(Gender, DateOfBirth)`.
- [x] Added synthetic performance test data.
- [x] Profiled the query using PostgreSQL execution plans.
- [x] Compared performance before and after index usage.
- [x] Documented measured performance improvement.
- [ ] Push Sprint 3 branch and open Pull Request.
