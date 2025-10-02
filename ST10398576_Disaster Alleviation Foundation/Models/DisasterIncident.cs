namespace ST10398576_Disaster_Alleviation_Foundation.Models
{
    public class DisasterIncident
    {
        public int DisasterIncidentID { get; set; }
        public int UserID { get; set; }
        public AppUser? Reporter { get; set; }

        public string DisasterIncidentType { get; set; } = string.Empty;
        public string DisasterIncidentLocation { get; set; } = string.Empty;
        public string DisasterIncidentDescription { get; set; } = string.Empty;
        public DateTime ReportDate { get; set; } = DateTime.Now;
        public string DisasterIncidentStatus { get; set; } = "Pending";
    }
}
