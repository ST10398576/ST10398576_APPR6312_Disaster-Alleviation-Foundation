using System;

namespace ST10398576_Disaster_Alleviation_Foundation.Models
{
    public class Dispatch
    {
        public int DispatchID { get; set; }
        public int ProjectID { get; set; }
        public Project? Project { get; set; }

        public string Details { get; set; } = string.Empty;
        public DateTime DispatchDate { get; set; } = DateTime.Now;
    }
}
