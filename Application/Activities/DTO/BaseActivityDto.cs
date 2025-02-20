using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Activities.DTO
{
    public class BaseActivityDto
    {
    public  string Title { get; set; } = "";
    public DateTime Date { get; set; }
// Data annotation for non-nullable
    public  string Description { get; set; }= "";

  // Data annotation for non-nullable
    public  string Category { get; set; }= "";
    // Data annotation for non-nullable
    public  string City { get; set; }= "";
   // Data annotation for non-nullable
    public string Venue { get; set; }= "";
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    }
}