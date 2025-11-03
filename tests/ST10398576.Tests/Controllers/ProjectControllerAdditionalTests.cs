using System.Linq;
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
    public class ProjectControllerAdditionalTests
    {
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