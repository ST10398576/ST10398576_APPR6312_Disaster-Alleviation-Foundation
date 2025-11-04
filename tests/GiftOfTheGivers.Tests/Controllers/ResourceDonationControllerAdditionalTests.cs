using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST10398576_Disaster_Alleviation_Foundation.Controllers;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using GiftOfTheGivers.Tests.TestHelpers;

namespace GiftOfTheGivers.Tests.Controllers
{
    [TestClass]
    public class ResourceDonationControllerAdditionalTests
    {
        [TestMethod]
        public void Details_ReturnsNotFound_WhenMissing()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var controller = new ResourceDonationController(ctx);

            var result = controller.Details(12345);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Create_ReturnsView_WhenModelStateInvalid()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var controller = new ResourceDonationController(ctx);
            controller.ModelState.AddModelError("ResourceDonationType", "Required");

            var model = new ResourceDonation();
            var result = controller.Create(model);

            result.Should().BeOfType<ViewResult>();
        }
    }
}