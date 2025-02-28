﻿using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// This class defines the database tables using Dbset<T> and the connection string to the database
namespace Persistence;


public class AppDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    
    // Create a table Activites that contains the properties specified in the activities class (Domain entitiy class)
    public required DbSet<Activity> Activities { get; set; } 
    
}

    

