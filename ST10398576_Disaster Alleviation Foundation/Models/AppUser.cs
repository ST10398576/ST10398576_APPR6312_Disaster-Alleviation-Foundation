using Microsoft.AspNetCore.Identity;

namespace ST10398576_Disaster_Alleviation_Foundation.Models
{
    // IdentityUser<int> so primary key is int
    public class AppUser : IdentityUser<int>
    {
        public string? FullName { get; set; }
    }
}