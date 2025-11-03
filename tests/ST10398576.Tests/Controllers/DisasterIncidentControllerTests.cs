using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST10398576_Disaster_Alleviation_Foundation.Controllers;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using GiftOfTheGivers.Tests.TestHelpers;

namespace GiftOfTheGivers.Tests.Controllers
{
    [TestClass]
    public class DisasterIncidentControllerTests
    {
        [TestMethod]
        public void Index_ReturnsIncidentList()
        {
            var ctx = InMemoryDbContextFactory.Create();
            ctx.DisasterIncidents.AddRange(
                new DisasterIncident { DisasterIncidentType = "Flood", DisasterIncidentLocation = "Cape Town", DisasterIncidentDescription = "Severe flooding" },
                new DisasterIncident { DisasterIncidentType = "Fire", DisasterIncidentLocation = "Durban", DisasterIncidentDescription = "Wildfire" }
            );
            ctx.SaveChanges();

            var controller = new DisasterIncidentController(ctx);
            var result = controller.Index() as ViewResult;

            result.Should().NotBeNull();
            var model = result!.Model as List<DisasterIncident>;
            model.Should().NotBeNull();
            model.Should().HaveCount(2);
        }

        [TestMethod]
        public void Create_ValidIncident_SavesAndRedirects()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var controller = new DisasterIncidentController(ctx);

            var incident = new DisasterIncident
            {
                DisasterIncidentType = "Flood",
                DisasterIncidentLocation = "Cape Town",
                DisasterIncidentDescription = "River overflow"
            };

            var result = controller.Create(incident) as RedirectToActionResult;

            result.Should().NotBeNull();
            result!.ActionName.Should().Be("Index");

            ctx.DisasterIncidents.Count().Should().Be(1);
        }

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