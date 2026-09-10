# Week 4 — Day 5: API Security Hardening

## Overview

This task focused on applying basic security hardening techniques to the ASP.NET Core Web API developed during Week 4.

## Topics Covered

- CORS configuration
- HTTPS redirection
- HSTS
- Rate limiting
- SQL Injection review

## Implemented Features

### 1. CORS

Configured a named CORS policy that allows requests only from a specific frontend origin.

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
```

The policy is enabled in the middleware pipeline using:

```csharp
app.UseCors("AllowFrontend");
```

### 2. HTTPS & HSTS

HTTPS redirection was enabled using:

```csharp
app.UseHttpsRedirection();
```

HSTS was configured for non-development environments:

```csharp
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
```

### 3. Rate Limiting

A fixed-window rate limiter was configured and applied specifically to the login endpoint.

```text
Limit: 5 requests
Window: 10 seconds
```

Requests exceeding the configured limit return:

```text
429 Too Many Requests
```

### 4. SQL Injection Review

The codebase was reviewed for raw SQL queries.

The following methods were checked:

```text
FromSqlRaw
ExecuteSqlRaw
```

No unsafe raw SQL queries using unparameterized string interpolation were found.

## Verification

The project was successfully built after applying the security configurations.

```text
Build succeeded
```

## Key Takeaways

- CORS controls which browser origins are allowed to access the API.
- HTTPS protects communication between the client and the API.
- HSTS instructs browsers to use HTTPS for the application.
- Rate limiting helps protect sensitive endpoints such as login from excessive requests.
- EF Core and LINQ were used instead of unsafe unparameterized raw SQL queries.