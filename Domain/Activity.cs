﻿using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Activity
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); 

     // Data annotation for non-nullable
    public required string Title { get; set; }

    public required DateTime Date { get; set; }

     // Data annotation for non-nullable
    public required string Description { get; set; }

     // Data annotation for non-nullable
    public required string Category { get; set; }

    public bool IsCancelled { get; set; }

    // Data annotation for non-nullable
    public required string City { get; set; }

    // Data annotation for non-nullable
    public required string Venue { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // Navigation Properties

    public ICollection<ActivityAttendee> Attendees {get; set;} = [];
}

