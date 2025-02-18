using System;
using System.Globalization;
using System.Text.Json.Serialization;
using Azure.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using User.API.Middleware;
using user_Core.IRepositories;
using user_Core.IService;
using user_Core.Mapper;
using user_Core.Service;
using User_InfraStructure.database;
using User_InfraStructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DBConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString,
        sqlOptions => sqlOptions.MigrationsAssembly("User_InfraStructure")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddMapster();
ProductMapper.ProductMappings();


var app = builder.Build();





////app.Use(async (context, next) =>
////{
////    Console.WriteLine($"Handling request for: { context.Request.Path}");
////    await next.Invoke();  // Continue to the next middleware
////});

////app.UseExceptionHandlingMiddleware();

//app.UseMiddleware<AuthorizingMiddleware>(); // Apply the custom middleware globally

//app.UseAuthorizingMiddleware();

//app.UseRequestCultureMiddleware();

////middleware for authentication
////app.UseAuthenticationMiddleware();





//// Use Map to handle requests for specific paths
//app.Map("/admin", adminApp =>
//{
//    adminApp.UseMiddleware<LoggingMiddleware>();  // You can add specific middleware for /admin
//    adminApp.Use(async (context, next) =>
//    {
//        Console.WriteLine("Handling request for /admin.");
//        await next.Invoke();  // Continue to the next middleware
//    });
//});

//app.Map("/user", userApp =>
//{
//    userApp.UseMiddleware<LoggingMiddleware>();  // You can add specific middleware for /user
//    userApp.Use(async (context, next) =>
//    {
//        Console.WriteLine("Handling request for /user.");
//        await next.Invoke();  // Continue to the next middleware
//    });
//});


//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseResponseHeaderMiddleware();

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ?? 1. Global Exception Handling (First to catch errors)
app.UseExceptionHandlingMiddleware();

// ?? 2. Request Logging Middleware (Logs all requests)
app.Use(async (context, next) =>
{
    Console.WriteLine($"Handling request for: {context.Request.Path}");
    await next.Invoke();  // Continue to the next middleware
});

// ?? 3. Security Middleware (HTTPS, Headers, Authentication)
app.UseHttpsRedirection();
app.UseResponseHeaderMiddleware();
app.UseAuthorization();  // Ensures that only authorized users can access protected routes

// ?? 4. Custom Middleware (Runs before request processing)
app.UseMiddleware<AuthorizingMiddleware>();
app.UseRequestCultureMiddleware();

// ?? 5. Route-based Middleware (Specific paths)
app.Map("/admin", adminApp =>
{
    adminApp.UseMiddleware<LoggingMiddleware>();
    adminApp.Use(async (context, next) =>
    {
        Console.WriteLine("Handling request for /admin.");
        await next.Invoke();
    });
});

app.Map("/user", userApp =>
{
    userApp.UseMiddleware<LoggingMiddleware>();
    userApp.Use(async (context, next) =>
    {
        Console.WriteLine("Handling request for /user.");
        await next.Invoke();
    });
});

// ?? 6. API Controllers (Must be at the bottom)
app.MapControllers();




app.Run();

