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
    public class DisasterIncidentControllerTests
    {
        private DRFoundationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<DRFoundationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // isolate every test
                .Options;

            return new DRFoundationDbContext(options);
        }

        [Fact]
        public void Index_ReturnsIncidentList()
        {
            using var context = CreateContext();
            context.DisasterIncidents.AddRange(
                new DisasterIncident { DisasterIncidentID = 1, UserID = 1, DisasterIncidentType = "Flood", DisasterIncidentLocation = "Cape Town", DisasterIncidentDescription = "Severe flooding" },
                new DisasterIncident { DisasterIncidentID = 2, UserID = 2, DisasterIncidentType = "Fire", DisasterIncidentLocation = "Durban", DisasterIncidentDescription = "Wildfire" }
            );
            context.SaveChanges();

            var controller = new DisasterIncidentController(context);
            var result = controller.Index() as ViewResult;

            result.Should().NotBeNull();
            var model = result!.Model as List<DisasterIncident>;
            model.Should().NotBeNull();
            model.Should().HaveCount(2);
        }

        [Fact]
        public void Create_ValidIncident_SavesAndRedirects()
        {
            using var context = CreateContext();
            var controller = new DisasterIncidentController(context);

            var incident = new DisasterIncident
            {
                UserID = 1,
                DisasterIncidentType = "Flood",
                DisasterIncidentLocation = "Cape Town",
                DisasterIncidentDescription = "River overflow"
            };

            var result = controller.Create(incident) as RedirectToActionResult;

            result.Should().NotBeNull();
            result!.ActionName.Should().Be("Index");

            context.DisasterIncidents.Count().Should().Be(1);
        }
    }
}
