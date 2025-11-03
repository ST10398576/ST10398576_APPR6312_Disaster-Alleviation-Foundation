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
    public class ResourceDonationControllerTests
    {
        [TestMethod]
        public void Index_ReturnsDonations()
        {
            var ctx = InMemoryDbContextFactory.Create();
            ctx.ResourceDonations.AddRange(
                new ResourceDonation { ResourceDonationAmount = 100, ResourceDonationType = "Food" },
                new ResourceDonation { ResourceDonationAmount = 250, ResourceDonationType = "Clothing" }
            );
            ctx.SaveChanges();

            var controller = new ResourceDonationController(ctx);
            var result = controller.Index() as ViewResult;

            result.Should().NotBeNull();
            var model = result!.Model as List<ResourceDonation>;
            model.Should().NotBeNull();
            model.Should().HaveCount(2);
        }

        [TestMethod]
        public void Details_ReturnsCorrectDonation()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var donation = new ResourceDonation { ResourceDonationAmount = 100, ResourceDonationType = "Food" };
            ctx.ResourceDonations.Add(donation);
            ctx.SaveChanges();

            var controller = new ResourceDonationController(ctx);
            var result = controller.Details(donation.ResourceDonationID) as ViewResult;

            result.Should().NotBeNull();
            var model = result!.Model as ResourceDonation;
            model.Should().NotBeNull();
            model!.ResourceDonationType.Should().Be("Food");
        }

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