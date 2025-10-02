using Microsoft.AspNetCore.Components;

namespace ST10398576_Disaster_Alleviation_Foundation.Models
{
    public class ProjectResource
    {
        public int ResourceID { get; set; }
        public string ResourceName { get; set; } = string.Empty;
        public int QuantityAvailable { get; set; }
        public string Category { get; set; } = string.Empty;
        public DateTime? ExpiryDate { get; set; }

        // Navigation
        public ICollection<Dispatch>? Dispatches { get; set; }
    }
}
