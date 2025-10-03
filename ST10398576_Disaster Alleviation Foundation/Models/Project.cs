using System;
using System.Collections.Generic;

namespace ST10398576_Disaster_Alleviation_Foundation.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string? ProjectDescription { get; set; }
        public string? ProjectLocation { get; set; }
        public string ProjectStatus { get; set; } = "Planned";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ICollection<ProjectVolunteer> Volunteers { get; set; } = new List<ProjectVolunteer>();
        public ICollection<Dispatch> Dispatches { get; set; } = new List<Dispatch>();
    }
}
