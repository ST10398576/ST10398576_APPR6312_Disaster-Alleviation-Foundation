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
    public class VolunteerControllerTests
    {
        [TestMethod]
        public void Index_ReturnsVolunteerList()
        {
            var ctx = InMemoryDbContextFactory.Create();
            ctx.Volunteers.AddRange(
                new Volunteer { VolunteerID = 1, FullName = "Alice", UserID = 1, Skills = "Cooking", Availability = "Weekends" },
                new Volunteer { VolunteerID = 2, FullName = "Bob", UserID = 2, Skills = "Driving", Availability = "Weekdays" }
            );
            ctx.SaveChanges();

            var controller = new VolunteerController(ctx);

            var result = controller.Index() as ViewResult;
            result.Should().NotBeNull();

            var model = result!.Model as List<Volunteer>;
            model.Should().NotBeNull();
            model.Should().HaveCount(2);
        }

        [TestMethod]
        public void Details_ReturnsCorrectVolunteer()
        {
            var ctx = InMemoryDbContextFactory.Create();

            // Seed related AppUser
            var user = new AppUser
            {
                Id = 1,
                FullName = "Alice",
                Email = "alice@example.com",
                UserName = "alice@example.com",
                NormalizedEmail = "ALICE@EXAMPLE.COM",
                NormalizedUserName = "ALICE@EXAMPLE.COM",
                EmailConfirmed = true
            };
            ctx.Users.Add(user);

            // Seed Volunteer with navigation property
            var volunteer = new Volunteer
            {
                VolunteerID = 1,
                FullName = "Alice",
                UserID = 1,
                Skills = "Cooking",
                Availability = "Weekends",
                User = user
            };
            ctx.Volunteers.Add(volunteer);
            ctx.SaveChanges();

            var controller = new VolunteerController(ctx);
            var result = controller.Details(volunteer.VolunteerID) as ViewResult;

            result.Should().NotBeNull();
            var model = result!.Model as Volunteer;
            model.Should().NotBeNull();
            model!.FullName.Should().Be("Alice");
        }

        [TestMethod]
        public void Register_ValidVolunteer_SavesAndRedirects()
        {
            var ctx = InMemoryDbContextFactory.Create();
            var controller = new VolunteerController(ctx);
            var volunteer = new Volunteer { FullName = "Alice", UserID = 1, Skills = "Cooking", Availability = "Weekends" };

            var result = controller.Register(volunteer) as RedirectToActionResult;

            result.Should().NotBeNull();
            result!.ActionName.Should().Be("Index");

            ctx.Volunteers.Count().Should().Be(1);
        }

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