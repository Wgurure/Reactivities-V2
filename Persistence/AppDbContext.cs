using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// This class defines the database tables using Dbset<T> and the connection string to the database
namespace Persistence;


public class AppDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    
    // Create a table Activites that contains the properties specified in the activities class (Domain entitiy class)
    public required DbSet<Activity> Activities { get; set; }
    public required DbSet<ActivityAttendee> ActivityAttendees { get; set; }
    public required DbSet<Photo> Photos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Sets the new ActivityAttendee to have a primary key made up of the Activity Id and the User Id 
        builder.Entity<ActivityAttendee>(x => x.HasKey(a => new { a.ActivityId, a.UserId }));

        // Configures a one-to-many relationship where one User can have many ActivityAttendees (can attend many activities).
        builder.Entity<ActivityAttendee>()
            .HasOne(x => x.User)
            .WithMany(x => x.Activities)
            .HasForeignKey(x => x.UserId);  // The UserId foreign key in ActivityAttendee links each attendee to a specific User.


        builder.Entity<ActivityAttendee>()
            .HasOne(x => x.Activity)
            .WithMany(x => x.Attendees)
            .HasForeignKey(x => x.ActivityId);
    }

}

    

