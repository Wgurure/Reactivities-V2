using System;
using System.Security.Claims;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Security;


// HttpCOntextAccessor: provides access to the current HTTP request context -> the user, request headers and cookies 
public class UserAccessor(IHttpContextAccessor httpContextAccessor, AppDbContext dbContext) 
    : IUserAccessor
{
// Asynchronously retrieves the user from the database using the user ID.
// Throws an UnauthorizedAccessException if no user is logged in or found in the database.
    public async Task<User> GetUserAsync()
    {
        return await dbContext.Users.FindAsync(GetUserId())
            ?? throw new UnauthorizedAccessException("No user is logged in");
    }
    

// Retrieves the current user's unique ID from the claims in the HTTP context.
// Throws an exception if no user is found (i.e., the user is not authenticated).
    public string GetUserId()
    {
        return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new Exception("No user found");
    }
}