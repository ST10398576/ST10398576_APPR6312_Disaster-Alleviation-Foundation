using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10398576_Disaster_Alleviation_Foundation.Models
{
    public class Dispatch
    {
        [Key]
        public int DispatchID { get; set; }

        [Display(Name = "Dispatch Date")]
        [DataType(DataType.Date)]
        public DateTime? DispatchDate { get; set; }

        [Required]
        [Display(Name = "Quantity Dispatched")]
        public int QuantityDispatched { get; set; }

        // Foreign Key → ResourceDonation
        [Display(Name = "Resource Donation")]
        public int? ResourceDonationID { get; set; }
        public ResourceDonation? ResourceDonation { get; set; }

        // Foreign Key → Project
        [Display(Name = "Project")]
        public int? ProjectID { get; set; }
        public Project? Project { get; set; }
    }
}
