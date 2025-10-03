namespace ST10398576_Disaster_Alleviation_Foundation.Models
{
    public class ProjectResource
    {
        public int ProjectResourceID { get; set; }
        public int ProjectID { get; set; }
        public Project? Project { get; set; }

        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
