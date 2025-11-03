using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using Xunit;

namespace ST10398576.Tests
{
    public class VolunteerTests
    {
        private DRFoundationDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DRFoundationDbContext>()
                .UseInMemoryDatabase("VolTestDb_" + System.Guid.NewGuid().ToString("N"))
                .Options;
            return new DRFoundationDbContext(options);
        }

        [Fact]
        public async Task AddVolunteer_SavesToDatabase()
        {
            using var ctx = CreateInMemoryContext();

            var user = new AppUser { Id = 1, FullName = "Alice", Email = "alice@example.com", UserName = "alice@example.com" };
            ctx.Users.Add(user);

            var vol = new Volunteer
            {
                VolunteerID = 1,
                FullName = "Alice",
                UserID = 1,
                Skills = "Cooking",
                Availability = "Weekends",
                User = user
            };

            ctx.Volunteers.Add(vol);
            ctx.SaveChanges();

            var fetched = await ctx.Volunteers.FirstOrDefaultAsync();
            fetched.Should().NotBeNull();
            fetched!.FullName.Should().Be("Alice");
            fetched.Skills.Should().Be("Cooking");
        }

        [Fact]
        public async Task UpdateVolunteer_ChangesPersist()
        {
            using var ctx = CreateInMemoryContext();

            var vol = new Volunteer
            {
                UserID = 2,
                FullName = "John Smith",
                Skills = "Driving",
                Availability = "Full-time"
            };

            ctx.Volunteers.Add(vol);
            await ctx.SaveChangesAsync();

            vol.Availability = "Part-time";
            ctx.Volunteers.Update(vol);
            await ctx.SaveChangesAsync();

            var updated = await ctx.Volunteers.FirstAsync();
            updated.Availability.Should().Be("Part-time");
        }

        [Fact]
        public async Task DeleteVolunteer_RemovesFromDatabase()
        {
            using var ctx = CreateInMemoryContext();

            var vol = new Volunteer
            {
                UserID = 3,
                FullName = "Test Delete",
                Skills = "Logistics",
                Availability = "Weekdays"
            };

            ctx.Volunteers.Add(vol);
            await ctx.SaveChangesAsync();

            ctx.Volunteers.Remove(vol);
            await ctx.SaveChangesAsync();

            var exists = await ctx.Volunteers.AnyAsync();
            exists.Should().BeFalse();
        }
    }
}
