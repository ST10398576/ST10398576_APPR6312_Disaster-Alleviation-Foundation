using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10398576_Disaster_Alleviation_Foundation.Models
{
    public class Dispatch
    {
        [Key]
        public int DispatchID { get; set; }

        // Link to the disaster incident this dispatch is for
        [Required]
        public int DisasterIncidentID { get; set; }
        public DisasterIncident? DisasterIncident { get; set; }

        // Link to the donated resource being dispatched
        [Required]
        public int ResourceDonationID { get; set; }
        public ResourceDonation? Resource { get; set; }

        // How many units/items/money dispatched
        [Required]
        public int QuantityDispatched { get; set; }

        [Required]
        public DateTime DispatchDate { get; set; } = DateTime.UtcNow;

        // Optional notes
        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}
