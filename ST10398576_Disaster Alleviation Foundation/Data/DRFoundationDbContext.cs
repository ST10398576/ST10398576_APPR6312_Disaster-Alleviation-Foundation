using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Models;

namespace ST10398576_Disaster_Alleviation_Foundation.Data
{
    public class DRFoundationDbContext : IdentityDbContext<AppUser, UserRole, int>
    {
        public DRFoundationDbContext(DbContextOptions<DRFoundationDbContext> options) : base(options) { }
        
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectVolunteer> ProjectVolunteers { get; set; }
        public DbSet<ProjectResource> ProjectResources { get; set; }
        public DbSet<Dispatch> Dispatches { get; set; }
        public DbSet<ResourceDonation> ResourceDonations { get; set; }
        public DbSet<DisasterIncident> DisasterIncidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique email enforced (optional)
            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Many-to-many mapping: ProjectVolunteer
            modelBuilder.Entity<ProjectVolunteer>()
                .HasOne(pv => pv.Project)
                .WithMany(p => p.Volunteers)
                .HasForeignKey(pv => pv.ProjectID);

            modelBuilder.Entity<ProjectVolunteer>()
                .HasOne(pv => pv.Volunteer)
                .WithMany(v => v.Assignments)
                .HasForeignKey(pv => pv.VolunteerID);

            // Dispatch → DisasterIncident
            modelBuilder.Entity<Dispatch>()
                .HasOne(d => d.DisasterIncident)
                .WithMany()
                .HasForeignKey(d => d.DisasterIncidentID)
                .OnDelete(DeleteBehavior.Cascade);

            // Dispatch → ResourceDonation
            modelBuilder.Entity<Dispatch>()
                .HasOne(d => d.Resource)
                .WithMany()
                .HasForeignKey(d => d.ResourceDonationID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}