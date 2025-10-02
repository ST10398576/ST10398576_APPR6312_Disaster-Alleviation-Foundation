namespace ST10398576_Disaster_Alleviation_Foundation.Models
{
    public class ProjectVolunteer
    {
        public int ProjectVolunteerID { get; set; }

        public int ProjectID { get; set; }
        public Project? Project { get; set; }

        public int VolunteerID { get; set; }
        public Volunteer? Volunteer { get; set; }

        public DateTime AssignmentDate { get; set; } = DateTime.Now;
    }
}
