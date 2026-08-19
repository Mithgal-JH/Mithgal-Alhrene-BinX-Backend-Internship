# Day 4 — Centralized Error Handling & Global Exception Middleware

## Overview

Implemented centralized error handling for the Cardiac Patient Monitoring System API using custom ASP.NET Core middleware.

The goal was to handle unexpected exceptions consistently, return standardized `ProblemDetails` responses, and log full exception details on the server without exposing sensitive information to API clients.

---

## What Was Implemented

### 1. Global Exception Handling Middleware

Created:

```text
Middleware/
└── ExceptionHandlingMiddleware.cs
```

The middleware catches unhandled exceptions from downstream components.

Key implementation:

```csharp
public async Task InvokeAsync(HttpContext context)
{
    try
    {
        await _next(context);
    }
    catch (Exception ex)
    {
        // Handle unexpected exception
    }
}
```

---

### 2. Structured Logging

Used `ILogger` with structured logging and request path context:

```csharp
_logger.LogError(
    ex,
    "Unhandled exception occurred while processing {RequestPath}",
    context.Request.Path);
```

This keeps the request path as a separate searchable property in the log entry.

---

### 3. ProblemDetails Response

Unexpected exceptions return a safe standardized response:

```csharp
var problemDetails = new ProblemDetails
{
    Title = "An unexpected error occurred.",
    Status = StatusCodes.Status500InternalServerError
};
```

Example response:

```json
{
  "title": "An unexpected error occurred.",
  "status": 500
}
```

The actual exception details remain on the server side.

---

### 4. Middleware Registration

Registered the middleware early in the ASP.NET Core request pipeline:

```csharp
app.UseMiddleware<ExceptionHandlingMiddleware>();
```

This allows the middleware to catch unhandled exceptions from downstream Controllers and Services.

---

## Testing

A temporary test endpoint was used to deliberately trigger an unhandled exception and verify the middleware behavior.

The test confirmed that:

- The server logs the full exception and stack trace.
- The request path is included in the structured log.
- The client receives `500 Internal Server Error`.
- The client receives a `ProblemDetails` response.
- The actual exception message and stack trace are not exposed to the client.

The test endpoint was commented out after verification.

---

## Evidence

### Client Response

The Postman response demonstrates the safe `ProblemDetails` response returned to the client.

![Client Response](Client%20Response.png)

### Server Log

The server log demonstrates that the full exception details were logged server-side using `ILogger`.

![Server Log](Server%20Log.png)

---

## Key Concepts

- Centralized Exception Handling
- ASP.NET Core Middleware
- `ProblemDetails`
- `ILogger`
- Structured Logging
- HTTP 500 Internal Server Error
- Secure Error Responses
- Exception Handling Pipeline

---

## Result

The API now has a centralized mechanism for handling unexpected exceptions instead of relying on repetitive `try/catch` blocks inside individual endpoints.

Expected errors such as `404 Not Found`, `401 Unauthorized`, and `403 Forbidden` continue to be handled by the appropriate endpoints, while unexpected exceptions are handled centrally by the global exception middleware.
