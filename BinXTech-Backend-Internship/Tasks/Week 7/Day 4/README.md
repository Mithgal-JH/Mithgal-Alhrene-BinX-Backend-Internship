# Week 7 --- Day 4

## Custom Middleware & Request Timing

### Overview

Today focused on implementing and testing a custom ASP.NET Core
Middleware for measuring API request execution time.

The goal was to understand how Middleware works as a cross-cutting
concern and how a single Middleware can monitor multiple API endpoints
without duplicating timing logic inside Controllers.

------------------------------------------------------------------------

## Objectives

-   Understand the ASP.NET Core Middleware pipeline.
-   Implement a custom `RequestTimingMiddleware`.
-   Use `RequestDelegate` to pass requests to the next Middleware.
-   Use `ILogger<T>` with Dependency Injection.
-   Measure request execution time using `Stopwatch`.
-   Use `try/finally` to ensure timing is logged even when an exception
    occurs.
-   Implement structured logging.
-   Test the Middleware with multiple API endpoints.
-   Understand Middleware ordering and its relationship with exception
    handling.

------------------------------------------------------------------------

## 1. RequestTimingMiddleware

A custom Middleware named `RequestTimingMiddleware` was implemented.

Its responsibility is to measure how long each HTTP request takes to
complete.

``` csharp
using System.Diagnostics;

namespace CardiacPatientMonitoringSystem.Middleware;

public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimingMiddleware> _logger;

    public RequestTimingMiddleware(
        RequestDelegate next,
        ILogger<RequestTimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            _logger.LogInformation(
                "HTTP {Method} {Path} responded {StatusCode} in {ElapsedMilliseconds} ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }
}
```

------------------------------------------------------------------------

## 2. How It Works

The Middleware starts a `Stopwatch` before passing the request to the
next component in the pipeline.

``` csharp
var stopwatch = Stopwatch.StartNew();
```

The request continues through the rest of the pipeline using:

``` csharp
await _next(context);
```

After the request finishes, the stopwatch is stopped and the execution
time is logged.

The `finally` block ensures that the timing logic executes even if an
exception occurs further down the pipeline.

------------------------------------------------------------------------

## 3. Dependency Injection and ILogger

The Middleware uses a typed logger:

``` csharp
ILogger<RequestTimingMiddleware>
```

instead of the non-generic:

``` csharp
ILogger
```

The typed logger allows ASP.NET Core Dependency Injection to resolve the
correct logger category for this Middleware.

Example log category:

``` text
CardiacPatientMonitoringSystem.Middleware.RequestTimingMiddleware
```

------------------------------------------------------------------------

## 4. Structured Logging

The Middleware uses structured logging:

``` csharp
_logger.LogInformation(
    "HTTP {Method} {Path} responded {StatusCode} in {ElapsedMilliseconds} ms",
    context.Request.Method,
    context.Request.Path,
    context.Response.StatusCode,
    stopwatch.ElapsedMilliseconds);
```

The log contains:

-   HTTP Method
-   Request Path
-   Response Status Code
-   Elapsed Time in milliseconds

Example:

``` text
HTTP GET /api/patients responded 200 in 158 ms
```

------------------------------------------------------------------------

## 5. Middleware Ordering

The existing `ExceptionHandlingMiddleware` is placed before the
`RequestTimingMiddleware`.

The pipeline is conceptually:

``` text
HTTP Request
     ↓
ExceptionHandlingMiddleware
     ↓
RequestTimingMiddleware
     ↓
Authentication / Authorization
     ↓
Controller
     ↓
HTTP Response
     ↑
RequestTimingMiddleware
     ↑
ExceptionHandlingMiddleware
```

This allows the exception handling Middleware to handle exceptions
raised by components further down the pipeline.

------------------------------------------------------------------------

## 6. Testing

The application was successfully built and executed using:

``` powershell
dotnet build
dotnet run
```

The custom Middleware was tested with multiple endpoints.

### Test Results

``` text
HTTP POST /api/auth/login responded 200 in 954 ms

HTTP GET /api/patientmedications/2 responded 200 in 361 ms

HTTP GET /api/patientmedications/2 responded 200 in 18 ms

HTTP GET /api/patientmedications responded 200 in 27 ms
```

These results confirm that the Middleware is applied globally to
different API requests.

------------------------------------------------------------------------

## 7. Key Learning

The main concept learned today is that Middleware is useful for
cross-cutting concerns.

Instead of adding timing logic to every Controller, one Middleware can
handle request timing for the entire API pipeline.

``` text
One Middleware
      ↓
Multiple Endpoints
      ↓
Consistent Request Timing
```

This keeps Controllers focused on their actual business responsibilities
and avoids duplicated infrastructure logic.

------------------------------------------------------------------------

## Conclusion

Week 7 Day 4 was completed successfully.

A custom `RequestTimingMiddleware` was implemented manually and
integrated into the ASP.NET Core request pipeline.

The Middleware successfully records:

-   HTTP Method
-   Request Path
-   Status Code
-   Request Execution Time

The implementation was tested successfully across multiple API
endpoints.
