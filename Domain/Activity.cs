using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Activity
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); 

    [Required] // Data annotation for non-nullable
    public string Title { get; set; }

    public DateTime Date { get; set; }

    [Required] // Data annotation for non-nullable
    public string Description { get; set; }

    [Required] // Data annotation for non-nullable
    public string Category { get; set; }

    public bool IsCancelled { get; set; }

    [Required] // Data annotation for non-nullable
    public string City { get; set; }

    [Required] // Data annotation for non-nullable
    public string Venue { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

