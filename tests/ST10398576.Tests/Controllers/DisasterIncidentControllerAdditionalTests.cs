using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST10398576_Disaster_Alleviation_Foundation.Controllers;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using GiftOfTheGivers.Tests.TestHelpers;

namespace GiftOfTheGivers.Tests.Controllers
{
    [TestClass]
    public class DisasterIncidentControllerAdditionalTests
    {
        [TestMethod]
        public void Details_ReturnsNotFound_WhenMissing()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var controller = new DisasterIncidentController(ctx);

            var result = controller.Details(9999);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Create_ReturnsView_WhenModelStateInvalid()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var controller = new DisasterIncidentController(ctx);
            controller.ModelState.AddModelError("DisasterIncidentType", "Required");

            var incident = new DisasterIncident();
            var result = controller.Create(incident);

            result.Should().BeOfType<ViewResult>();
        }
    }
}