using System;
using Application.Profiles.DTOs;

namespace Application.Activities.DTO;

public class ActivityDto
{
    public required string Id { get; set; }  

     // Data annotation for non-nullable
    public required string Title { get; set; }

    public required DateTime Date { get; set; }

     // Data annotation for non-nullable
    public required string Description { get; set; }

     // Data annotation for non-nullable
    public required string Category { get; set; }

    public bool IsCancelled { get; set; }
    public required string HostDisplayName { get; set; }
    public required string HostId { get; set; }

    // Data annotation for non-nullable
    public required string City { get; set; }

    // Data annotation for non-nullable
    public required string Venue { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // Navigation Properties

    public ICollection<UserProfile> Attendees {get; set;} = [];
}
