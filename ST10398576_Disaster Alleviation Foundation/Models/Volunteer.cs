namespace ST10398576_Disaster_Alleviation_Foundation.Models
{
    public class Volunteer
    {
        public int VolunteerID { get; set; }
        public int UserID { get; set; }
        public AppUser? User { get; set; }

        public string Skills { get; set; } = string.Empty;
        public string Availability { get; set; } = string.Empty;

        public ICollection<ProjectVolunteer> Assignments { get; set; } = new List<ProjectVolunteer>();
    }
}
