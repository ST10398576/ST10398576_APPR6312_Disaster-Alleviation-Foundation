using System;

namespace ST10398576_Disaster_Alleviation_Foundation.Models
{
    public class ResourceDonation
    {
        public int ResourceDonationID { get; set; }
        public int UserID { get; set; }            
        public AppUser? Donor { get; set; }

        public decimal ResourceDonationAmount { get; set; }
        public string ResourceDonationType { get; set; } = string.Empty;
        public string? ResourceItemDescription { get; set; }
        public int? ResourceDonationQuantity { get; set; }
        public DateTime ResourceDonationDate { get; set; } = DateTime.Now;
        public string? ResourceDonationPaymentReference { get; set; }
    }
}
