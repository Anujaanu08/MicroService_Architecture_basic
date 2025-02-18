using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace User.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ResponseHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Call the next middleware
            await _next(context);

            // Add custom header to the response
            context.Response.Headers.Add("X-Custom-Header", "This is a custom header");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ResponseHeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseHeaderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseHeaderMiddleware>();
        }
    }
}
