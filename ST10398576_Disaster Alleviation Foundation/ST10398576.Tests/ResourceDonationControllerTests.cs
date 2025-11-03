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
    public class ResourceDonationControllerTests
    {
        private DRFoundationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<DRFoundationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new DRFoundationDbContext(options);
        }

        [Fact]
        public void Index_ReturnsDonations()
        {
            using var context = CreateContext();
            context.ResourceDonations.AddRange(
                new ResourceDonation { ResourceDonationID = 1, UserID = 1, ResourceDonationAmount = 100, ResourceDonationType = "Food" },
                new ResourceDonation { ResourceDonationID = 2, UserID = 2, ResourceDonationAmount = 250, ResourceDonationType = "Clothing" }
            );
            context.SaveChanges();

            var controller = new ResourceDonationController(context);
            var result = controller.Index() as ViewResult;

            result.Should().NotBeNull();
            var model = result!.Model as List<ResourceDonation>;
            model.Should().NotBeNull();
            model.Should().HaveCount(2);
        }

        [Fact]
        public void Details_ReturnsCorrectDonation()
        {
            using var context = CreateContext();
            var donation = new ResourceDonation { ResourceDonationID = 1, UserID = 1, ResourceDonationAmount = 100, ResourceDonationType = "Food" };
            context.ResourceDonations.Add(donation);
            context.SaveChanges();

            var controller = new ResourceDonationController(context);
            var result = controller.Details(1) as ViewResult;

            result.Should().NotBeNull();
            var model = result!.Model as ResourceDonation;
            model.Should().NotBeNull();
            model!.ResourceDonationType.Should().Be("Food");
        }
    }
}
