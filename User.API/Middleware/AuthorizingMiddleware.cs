using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace User.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthorizingMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the path starts with /admin
            if (context.Request.Path.StartsWithSegments("/admin"))
            {
                // If it's an /admin request, apply some admin-specific logic
                Console.WriteLine("Admin request received.");
                // You can add further middleware logic here or call the next middleware
                await _next(context);
            }
            else if (context.Request.Path.StartsWithSegments("/user"))
            {
                // If it's a /user request, apply user-specific logic
                Console.WriteLine("User request received.");
                await _next(context);
            }
            else
            {
                // Default handling if no specific path is matched
                await _next(context);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthorizingMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthorizingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizingMiddleware>();
        }
    }
}
