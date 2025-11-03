using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST10398576_Disaster_Alleviation_Foundation.Controllers;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using GiftOfTheGivers.Tests.TestHelpers;

namespace GiftOfTheGivers.Tests.Controllers
{
    [TestClass]
    public class VolunteerControllerAdditionalTests
    {
        [TestMethod]
        public void Details_ReturnsNotFound_WhenMissing()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var controller = new VolunteerController(ctx);

            var result = controller.Details(5555);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Register_ReturnsView_WhenModelStateInvalid()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var controller = new VolunteerController(ctx);
            controller.ModelState.AddModelError("FullName", "Required");

            var v = new Volunteer();
            var result = controller.Register(v);

            result.Should().BeOfType<ViewResult>();
        }
    }
}