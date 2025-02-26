using API.Middleware;
using Application.Activities.Queries;
using Application.Activities.Validators;
using Application.Core;
using Domain;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt => 
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});
// Whenever wee need to acccess and communicate with the db we should create a new instatnce of the AppDbContext using this connection string provided in the appsettings.json
builder.Services.AddDbContext<AppDbContext>(opt => { 
    // Go into our app settings and view the connection string that we have set up
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); 
});

builder.Services.AddCors();

builder.Services.AddMediatR(x => {
    x.RegisterServicesFromAssemblyContaining<GetACtivityList.Handler>(); //Configuring Mediator to use the handlers when processing requests
    x.AddOpenBehavior(typeof(ValidationBehaviour<,>));
}); // Add the MediatR service to the container

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly); // Add the AutoMapper service to the container
builder.Services.AddValidatorsFromAssemblyContaining<CreateActivityValidator>();
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddIdentityApiEndpoints<User>(
    opt => {
        opt.User.RequireUniqueEmail = true;
    }
).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();






/******************************************** */
// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

// allows all the http requests coming from our front end to be accepted by the backend without cors blocking it 
app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
     .AllowCredentials()
    .WithOrigins("http://localhost:3000","https://localhost:3000")); 


app.UseAuthentication();
app.UseAuthorization();   
app.MapControllers(); // ROuting for the controllers
app.MapGroup("api").MapIdentityApi<User>();

using var scope = app.Services.CreateScope(); // Create a scope for the service

var services = scope.ServiceProvider; // Get the service provider

// Creates a temporary scope for the AppDbContext, applies pending migrations, seeds data, and disposes of the context to release resources.
try {
    

    // Get the context from the dependency injection container that contains the already configured connection string and set it as the context
    var context = services.GetRequiredService<AppDbContext>(); 
   var userManager = services.GetRequiredService<UserManager<User>>(); 
    // Apply any pending migrations to the database  
    await context.Database.MigrateAsync(); 
    //using the DbInitializer to check if the db has data and if not then seed data using the provided context
    await DbInitializer.SeedData(context, userManager); 
}
catch (Exception ex) {
    var logger = services.GetRequiredService<ILogger<Program>>(); // Get the logger
    logger.LogError(ex, "An error occurred during migrations"); // Log the error
}

app.Run();
