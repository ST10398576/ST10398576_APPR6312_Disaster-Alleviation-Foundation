namespace ST10398576_Disaster_Alleviation_Foundation.Models
{
    public class AppUser
    {
        public int UserID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Foreign key
        public int RoleID { get; set; }
        public UserRole? Role { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
