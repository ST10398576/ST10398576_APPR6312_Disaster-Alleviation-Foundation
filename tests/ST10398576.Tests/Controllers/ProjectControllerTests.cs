using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST10398576_Disaster_Alleviation_Foundation.Controllers;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using GiftOfTheGivers.Tests.TestHelpers;

namespace GiftOfTheGivers.Tests.Controllers
{
    [TestClass]
    public class ProjectControllerTests
    {
        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        // Additional negative/edge tests

        [TestMethod]
        public async Task Details_ReturnsNotFound_WhenMissing()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var controller = new ProjectController(ctx);

            var result = await controller.Details(9999);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task Create_ReturnsView_WhenModelStateInvalid()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var controller = new ProjectController(ctx);
            controller.ModelState.AddModelError("ProjectName", "Required");

            var result = await controller.Create(new Project());

            result.Should().BeOfType<ViewResult>();
            var view = result as ViewResult;
            view!.Model.Should().BeOfType<Project>();
        }

        [TestMethod]
        public async Task Edit_ReturnsNotFound_WhenIdMismatch()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var controller = new ProjectController(ctx);

            var project = new Project { ProjectID = 2, ProjectName = "X" };
            var result = await controller.Edit(1, project);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task Edit_SavesChanges_WhenValid()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var p = new Project { ProjectName = "Initial", ProjectDescription = "d" };
            ctx.Projects.Add(p);
            await ctx.SaveChangesAsync();

            p.ProjectName = "Updated";
            var controller = new ProjectController(ctx);

            var result = await controller.Edit(p.ProjectID, p);

            result.Should().BeOfType<RedirectToActionResult>();
            ctx.Projects.First().ProjectName.Should().Be("Updated");
        }

        [TestMethod]
        public async Task Delete_ReturnsNotFound_WhenMissing()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var controller = new ProjectController(ctx);

            var result = await controller.Delete(8888);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task DeleteConfirmed_RemovesEntity_AndRedirects()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var p = new Project { ProjectName = "ToDelete" };
            ctx.Projects.Add(p);
            await ctx.SaveChangesAsync();

            var controller = new ProjectController(ctx);
            var result = await controller.DeleteConfirmed(p.ProjectID);

            result.Should().BeOfType<RedirectToActionResult>();
            ctx.Projects.Any().Should().BeFalse();
        }
    }
}