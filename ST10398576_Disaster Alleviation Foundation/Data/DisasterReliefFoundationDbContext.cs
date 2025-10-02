using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Models;

namespace ST10398576_Disaster_Alleviation_Foundation.Data
{
    public class DRFoundationDbContext : DbContext
    {
        public DRFoundationDbContext(DbContextOptions<DRFoundationDbContext> options) : base(options) { }
        
        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectVolunteer> ProjectVolunteers { get; set; }
        public DbSet<ProjectResource> ProjectResources { get; set; }
        public DbSet<Dispatch> Dispatches { get; set; }
        public DbSet<ResourceDonation> Donations { get; set; }
        public DbSet<DisasterIncident> Incidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example: enforce unique email
            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Many-to-many (Project ↔ Volunteer via ProjectVolunteer)
            modelBuilder.Entity<ProjectVolunteer>()
                .HasOne(pv => pv.Project)
                .WithMany(p => p.Volunteers)
                .HasForeignKey(pv => pv.ProjectID);

            modelBuilder.Entity<ProjectVolunteer>()
                .HasOne(pv => pv.Volunteer)
                .WithMany(v => v.Assignments)
                .HasForeignKey(pv => pv.VolunteerID);
        }
    }
}