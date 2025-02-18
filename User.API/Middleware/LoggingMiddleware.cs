using System.Diagnostics;

namespace User.API.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            Debug.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

            // Call the next middleware
            await _next(context);

            // Log Response details after passing through other middlewares
            Debug.WriteLine($"Response Status Code: {context.Response.StatusCode}");
        }

    }
}
