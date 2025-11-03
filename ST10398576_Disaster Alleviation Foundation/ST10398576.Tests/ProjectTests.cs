using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using Xunit;

namespace ST10398576.Tests
{
    public class ProjectTests
    {
        private DRFoundationDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DRFoundationDbContext>()
                .UseInMemoryDatabase("ProjTestDb_" + Guid.NewGuid().ToString("N"))
                .Options;
            return new DRFoundationDbContext(options);
        }

        [Fact]
        public async Task AddProject_SavesToDatabase_AndDefaultsStatusToPlanned()
        {
            using var ctx = CreateInMemoryContext();

            var proj = new Project
            {
                ProjectName = "Flood Relief",
                ProjectDescription = "Provide shelter and food to flood victims",
                ProjectLocation = "Cape Town",
                StartDate = DateTime.UtcNow
            };

            ctx.Projects.Add(proj);
            await ctx.SaveChangesAsync();

            var fetched = await ctx.Projects.FirstOrDefaultAsync();
            fetched.Should().NotBeNull();
            fetched!.ProjectName.Should().Be("Flood Relief");
            fetched.ProjectStatus.Should().Be("Planned");
        }

        [Fact]
        public async Task UpdateProject_ChangesPersistInDatabase()
        {
            using var ctx = CreateInMemoryContext();

            var proj = new Project
            {
                ProjectName = "Health Outreach",
                ProjectDescription = "Medical aid project",
                ProjectLocation = "Durban"
            };
            ctx.Projects.Add(proj);
            await ctx.SaveChangesAsync();

            proj.ProjectStatus = "In Progress";
            ctx.Projects.Update(proj);
            await ctx.SaveChangesAsync();

            var updated = await ctx.Projects.FirstAsync();
            updated.ProjectStatus.Should().Be("In Progress");
        }

        [Fact]
        public async Task DeleteProject_RemovesFromDatabase()
        {
            using var ctx = CreateInMemoryContext();

            var proj = new Project
            {
                ProjectName = "Test Deletion",
                ProjectDescription = "Delete test project",
                ProjectLocation = "Pretoria"
            };
            ctx.Projects.Add(proj);
            await ctx.SaveChangesAsync();

            ctx.Projects.Remove(proj);
            await ctx.SaveChangesAsync();

            var exists = await ctx.Projects.AnyAsync();
            exists.Should().BeFalse();
        }
    }
}
