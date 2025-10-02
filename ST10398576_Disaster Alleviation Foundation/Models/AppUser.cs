using Microsoft.AspNetCore.Identity;

namespace ST10398576_Disaster_Alleviation_Foundation.Models
{
    public class AppUser : IdentityUser<int>
    {
        public int UserID { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string PasswordHash { get; set; } = string.Empty;

        // Link to Role (navigation property)
        public int RoleID { get; set; }
        public UserRole? Role { get; set; }
    }
}
