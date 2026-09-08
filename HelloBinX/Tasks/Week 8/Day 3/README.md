# Day 3 — Introducing Redis Caching

## Overview

This day focused on introducing Redis caching in the ASP.NET Core application using the `IDistributedCache` abstraction.

The main goal was to reduce unnecessary database queries for frequently requested patient data by applying the **Cache-Aside Pattern** and ensuring that cached data is invalidated whenever the underlying patient data changes.

The implementation was applied to the **Patients** resource in the Cardiac Patient Monitoring System.

---

## Learning Objectives

By the end of this day, I was able to:

- Identify which data genuinely belongs in a cache.
- Set up Redis with ASP.NET Core using `IDistributedCache`.
- Implement the Cache-Aside Pattern.
- Handle cache invalidation when data is created, updated, or deleted.
- Verify the difference between cache misses and cache hits.
- Measure the response-time difference between cached and database requests.

---

# 1. What Belongs in a Cache

Good caching candidates are generally data that:

- Is read frequently.
- Changes relatively rarely.
- Can tolerate a short period of cached data.

Examples include:

- Product catalog listings.
- Category trees.
- Frequently requested read-only or mostly-read data.

Data that changes constantly and must always be current is usually not a good caching candidate.

Examples include:

- Live shopping carts.
- Frequently changing order status.
- Other highly dynamic data.

Caching frequently changing data without a proper invalidation strategy can result in stale data.

For this implementation, the paginated **Patients list endpoint** was selected as the caching target.

---

# 2. Setting Up Redis with IDistributedCache

Redis was integrated into the ASP.NET Core application using the StackExchange.Redis-backed `IDistributedCache` implementation.

### Redis Cache Registration

```csharp
builder.Services.AddStackExchangeRedisCache(options =>
    options.Configuration =
        builder.Configuration.GetConnectionString("Redis"));
```

A Redis connection multiplexer was also registered for operations that require direct Redis functionality:

```csharp
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(
        builder.Configuration.GetConnectionString("Redis")!));
```

Using `IDistributedCache` keeps the application caching code independent from the Redis client implementation for normal cache operations.

---

# 3. Cache-Aside Pattern

The implemented caching strategy follows the **Cache-Aside Pattern**.

The process is:

```text
Request
   ↓
Check Redis Cache
   ↓
Cache Hit?
 ┌───────┴───────┐
Yes              No
 ↓                ↓
Return Cache    Query Database
                  ↓
              Store Result
                  ↓
              Return Result
```

For the Patients endpoint:

```csharp
var cached = await _cache.GetStringAsync(cacheKey);

if (cached is not null)
{
    return JsonSerializer.Deserialize<
        PaginatedResponseDto<PatientResponseDto>>(cached)!;
}
```

If the cache does not contain the requested data, the application queries the database:

```csharp
var patients = await _repository.GetAllAsync(
    page,
    pageSize,
    search,
    gender,
    sort);
```

The result is then stored in Redis:

```csharp
await _cache.SetStringAsync(
    cacheKey,
    JsonSerializer.Serialize(patients),
    new DistributedCacheEntryOptions
    {
        AbsoluteExpirationRelativeToNow =
            TimeSpan.FromMinutes(10)
    });
```

The cache entries therefore have an expiration time of **10 minutes**.

---

# 4. Cache Key Design

The cache key includes the parameters that affect the returned patient list:

```csharp
var cacheKey =
    $"patients:v{version}:{page}:{pageSize}:" +
    $"{normalizedSearch}:{normalizedGender}:{normalizedSort}";
```

The key contains:

- Cache version.
- Page number.
- Page size.
- Search value.
- Gender filter.
- Sort option.

This prevents different requests from incorrectly sharing the same cached response.

---

# 5. Cache Versioning and Invalidation

A cache invalidation strategy was implemented using a Redis-based version value.

The version is stored using a key such as:

```text
patients:version
```

The current version is retrieved before creating the cache key.

```csharp
var version =
    await _cacheVersionService.GetVersionAsync("patients");
```

When patient data changes, the version is incremented.

```csharp
await _cacheVersionService.InvalidateAsync("patients");
```

The cache version service uses Redis `INCR` functionality:

```csharp
var newVersion =
    await database.StringIncrementAsync(key);
```

This means that after a write operation, new requests use a different cache key.

Example:

```text
patients:v3:1:10:::name
```

After invalidation:

```text
patients:v4:1:10:::name
```

The old cached result is therefore no longer used.

---

# 6. Cache Invalidation on Writes

Cache invalidation was added to all patient write operations.

## Create

After creating a patient:

```csharp
await _repository.AddAsync(patient);

await _cacheVersionService.InvalidateAsync("patients");
```

Flow:

```text
POST Patient
     ↓
Database INSERT
     ↓
Cache Invalidation
```

---

## Update

After updating a patient:

```csharp
await _repository.UpdateAsync(patient);

await _cacheVersionService.InvalidateAsync("patients");
```

Flow:

```text
PUT Patient
     ↓
Database UPDATE
     ↓
Cache Invalidation
```

---

## Delete

After deleting a patient:

```csharp
await _repository.DeleteAsync(patient);

await _cacheVersionService.InvalidateAsync("patients");
```

Flow:

```text
DELETE Patient
     ↓
Database DELETE
     ↓
Cache Invalidation
```

A cache without an invalidation strategy can return stale data after a write operation.

---

# 7. Hands-On Lab — Cache the Patients Endpoint

The official catalog caching exercise was applied to the `Patients` resource in the Cardiac Patient Monitoring System.

### Steps Completed

1. Configured Redis and registered `IDistributedCache`.
2. Implemented Cache-Aside caching for the patient list endpoint.
3. Added a 10-minute cache expiration.
4. Added cache invalidation for patient creation.
5. Added cache invalidation for patient updates.
6. Added cache invalidation for patient deletion.
7. Tested cache misses and cache hits using Postman.
8. Verified database queries through the ASP.NET Core console logs.
9. Verified the cache invalidation behavior after write operations.

---

# 8. Testing Results

## Test 1 — Initial GET

The first request to:

```http
GET /api/patients
```

resulted in database queries:

```text
SELECT count(*) FROM "Patients"

SELECT ...
FROM "Patients"
ORDER BY "DateOfBirth"
LIMIT @p1 OFFSET @p
```

Response time:

```text
HTTP GET /api/patients responded 200 in 783 ms
```

This represents a **Cache Miss**:

```text
GET
 ↓
Cache Miss
 ↓
Database
 ↓
Redis Cache
```

---

## Test 2 — Second GET

The same request was sent again:

```http
GET /api/patients
```

This time no new Patients database query appeared.

Response:

```text
HTTP GET /api/patients responded 200 in 34 ms
```

This confirmed a **Cache Hit**:

```text
GET
 ↓
Cache Hit
 ↓
Return Cached Data
```

---

## Test 3 — Update Patient

Patient `15` was updated using:

```http
PUT /api/patients/15
```

The database executed:

```text
UPDATE "Patients"
SET ...
WHERE "PatientId" = @p5;
```

The cache version was then incremented:

```text
Cache version for patients incremented to 3
```

Response:

```text
HTTP PUT /api/patients/15 responded 200 in 494 ms
```

This confirmed:

```text
PUT
 ↓
Database UPDATE
 ↓
Cache Invalidation
```

---

## Test 4 — GET After Update

After the update, the same GET request was executed again.

Database queries appeared again:

```text
SELECT count(*) FROM "Patients"

SELECT ...
FROM "Patients"
ORDER BY "DateOfBirth"
LIMIT @p1 OFFSET @p
```

Response:

```text
HTTP GET /api/patients responded 200 in 16 ms
```

This confirmed that the previous cached result was no longer used.

```text
GET
 ↓
Cache Miss
 ↓
Database
 ↓
New Cache Entry
```

---

## Test 5 — GET After Re-Caching

The same GET request was executed once more.

No new Patients SELECT appeared.

Response:

```text
HTTP GET /api/patients responded 200 in 3 ms
```

This confirmed another **Cache Hit**.

---

## Test 6 — Create Patient

A new patient was created using:

```http
POST /api/patients
```

The database executed:

```text
INSERT INTO "Patients"
```

The request returned:

```text
HTTP POST /api/patients responded 201 in 295 ms
```

The cache version was incremented:

```text
Cache version for patients incremented to 4
```

This confirmed:

```text
POST
 ↓
Database INSERT
 ↓
Cache Invalidation
```

A subsequent GET caused a new database query, confirming that the previous cached result was invalidated.

---

## Test 7 — Delete Patient

The test patient with:

```text
patientId = 17
```

was deleted using:

```http
DELETE /api/patients/17
```

The database executed:

```text
DELETE FROM "Patients"
WHERE "PatientId" = @p0;
```

The cache version was incremented:

```text
Cache version for patients incremented to 5
```

The request returned:

```text
HTTP DELETE /api/patients/17 responded 204 in 83 ms
```

This confirmed:

```text
DELETE
 ↓
Database DELETE
 ↓
Cache Invalidation
```

---

# 9. Final Cache Flow

The final implementation follows this flow:

```text
                GET /api/patients
                       │
                       ▼
                 Check Redis
                       │
              ┌────────┴────────┐
              │                 │
          Cache Hit         Cache Miss
              │                 │
              ▼                 ▼
        Return Cache       Query PostgreSQL
                                │
                                ▼
                           Store in Redis
                                │
                                ▼
                           Return Result
```

For write operations:

```text
POST / PUT / DELETE
          │
          ▼
      PostgreSQL
          │
          ▼
   Increment Cache Version
          │
          ▼
    Old Cache Ignored
```

---

# 10. Technologies Used

- C#
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Redis
- `IDistributedCache`
- StackExchange.Redis
- Postman

---

# 11. Key Takeaways

- Not all application data should be cached.
- Frequently read and relatively stable data is a good caching candidate.
- `IDistributedCache` provides a simple abstraction for distributed caching.
- The Cache-Aside Pattern checks the cache before querying the database.
- Cache invalidation is required when cached data changes.
- Cache keys must include parameters that affect the returned data.
- Cache hits can significantly reduce unnecessary database queries.
- The Patients endpoint successfully demonstrated cache miss, cache hit, and cache invalidation behavior.
