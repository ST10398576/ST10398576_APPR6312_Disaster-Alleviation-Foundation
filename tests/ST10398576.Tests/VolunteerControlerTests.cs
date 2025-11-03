using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Controllers;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using System.Collections.Generic;
using System;

namespace ST10398576.Tests
{
    public class VolunteerControllerTests
    {
        private DRFoundationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<DRFoundationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // unique DB each test
                .Options;

            return new DRFoundationDbContext(options);
        }

        [Fact]
        public void Index_ReturnsVolunteerList()
        {
            using var context = CreateContext();
            context.Volunteers.AddRange(
                new Volunteer { VolunteerID = 1, FullName = "Alice", UserID = 1, Skills = "Cooking", Availability = "Weekends" },
                new Volunteer { VolunteerID = 2, FullName = "Bob", UserID = 2, Skills = "Driving", Availability = "Weekdays" }
            );
            context.SaveChanges();

            var controller = new VolunteerController(context);

            var result = controller.Index() as ViewResult;
            result.Should().NotBeNull();

            var model = result!.Model as List<Volunteer>;
            model.Should().NotBeNull();
            model.Should().HaveCount(2);
        }

        [Fact]
        public void Details_ReturnsCorrectVolunteer()
        {
            using var context = CreateContext();

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
            context.Users.Add(user);

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
            context.Volunteers.Add(volunteer);
            context.SaveChanges();

            var controller = new VolunteerController(context);
            var result = controller.Details(1) as ViewResult;

            result.Should().NotBeNull();
            var model = result!.Model as Volunteer;
            model.Should().NotBeNull();
            model!.FullName.Should().Be("Alice");
        }

        [Fact]
        public void Register_ValidVolunteer_SavesAndRedirects()
        {
            using var context = CreateContext();
            var controller = new VolunteerController(context);
            var volunteer = new Volunteer { FullName = "Alice", UserID = 1, Skills = "Cooking", Availability = "Weekends" };

            var result = controller.Register(volunteer) as RedirectToActionResult;

            result.Should().NotBeNull();
            result!.ActionName.Should().Be("Index");

            context.Volunteers.Count().Should().Be(1);
        }
    }
}
