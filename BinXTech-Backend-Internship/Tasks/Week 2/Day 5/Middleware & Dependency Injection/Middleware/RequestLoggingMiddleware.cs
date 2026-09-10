namespace HelloBinX_Day5.Middleware;

// Custom middleware that logs every incoming HTTP request
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // Log the request method and path, then continue the pipeline
    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

        await _next(context);
    }
}