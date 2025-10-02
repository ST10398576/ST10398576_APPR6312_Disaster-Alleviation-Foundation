using Microsoft.AspNetCore.Components;

namespace ST10398576_Disaster_Alleviation_Foundation.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Location { get; set; }
        public string Status { get; set; } = "Planned"; // Planned, Ongoing, Completed

        // Navigation
        public ICollection<ProjectVolunteer>? Volunteers { get; set; }
        public ICollection<Dispatch>? Dispatches { get; set; }
    }
}
