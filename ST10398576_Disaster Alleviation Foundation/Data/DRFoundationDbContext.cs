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
        public DbSet<ResourceDonation> ResourceDonations { get; set; }
        public DbSet<DisasterIncident> DisasterIncidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique email enforced 
            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Email)
                .IsUnique();
            
            modelBuilder.Entity<ResourceDonation>()
                .Property(r => r.ResourceDonationAmount)
                .HasColumnType("decimal(18,2)");
        }
    }
}