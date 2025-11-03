using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ST10398576_Disaster_Alleviation_Foundation.Controllers;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using ST10398576.Tests.TestHelpers;
using Xunit;

namespace ST10398576.Tests
{
    public class ProjectControllerTests
    {
        [Fact]
        public async Task Create_ValidProject_SavesAndRedirects()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var controller = new ProjectController(ctx);

            var project = new Project { ProjectName = "Test Project", ProjectDescription = "desc" };

            var result = await controller.Create(project);
            result.Should().BeOfType<RedirectToActionResult>();

            var list = ctx.Projects.ToList();
            list.Should().ContainSingle();
            list[0].ProjectName.Should().Be("Test Project");
        }

        [Fact]
        public async Task Index_ReturnsProjectsList()
        {
            var ctx = InMemoryDbContextFactory.Create();
            ctx.Projects.Add(new Project { ProjectName = "A" });
            ctx.Projects.Add(new Project { ProjectName = "B" });
            await ctx.SaveChangesAsync();

            var controller = new ProjectController(ctx);

            var result = await controller.Index();
            var view = result as ViewResult;
            view.Should().NotBeNull();

            var model = view!.Model as List<Project>;
            model!.Count.Should().Be(2);
        }

        [Fact]
        public async Task Details_ReturnsCorrectProject()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var p = new Project { ProjectName = "X" };
            ctx.Projects.Add(p);
            await ctx.SaveChangesAsync();

            var controller = new ProjectController(ctx);
            var result = await controller.Details(p.ProjectID);
            var view = result as ViewResult;
            view.Should().NotBeNull();

            var model = view!.Model as Project;
            model.Should().NotBeNull();
            model!.ProjectName.Should().Be("X");
        }
    }
}
