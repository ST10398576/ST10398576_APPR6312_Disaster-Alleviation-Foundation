using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Models;

namespace ST10398576_Disaster_Alleviation_Foundation.Data
{
    public class DRFoundationDbContext : IdentityDbContext<AppUser>
    {
        public DRFoundationDbContext(DbContextOptions<DRFoundationDbContext> options) : base(options) { }

        public DbSet<ResourceDonation> ResourceDonations { get; set; }
        public DbSet<DisasterIncident> DisasterIncidents { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<ProjectVolunteer> ProjectVolunteers { get; set; }
    }
}