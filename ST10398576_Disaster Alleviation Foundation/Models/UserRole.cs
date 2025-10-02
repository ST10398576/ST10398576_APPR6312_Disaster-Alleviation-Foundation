namespace ST10398576_Disaster_Alleviation_Foundation.Models
{
    public class UserRole
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; } = string.Empty;

        public ICollection<AppUser>? Users { get; set; }
    }
}
