namespace ST10398576_Disaster_Alleviation_Foundation.Models
{
    public class Dispatch
    {
        public int DispatchID { get; set; }

        public int ProjectID { get; set; }
        public Project? Project { get; set; }

        public int ResourceID { get; set; }
        public ProjectResource? Resource { get; set; }

        public int QuantityDispatched { get; set; }
        public DateTime DispatchDate { get; set; } = DateTime.Now;
    }
}
