using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST10398576_Disaster_Alleviation_Foundation.Models;

namespace GiftOfTheGivers.Tests.Models
{
    [TestClass]
    public class ModelDefaultsTests
    {
        [TestMethod]
        public void Project_DefaultStatus_IsPlanned()
        {
            var p = new Project();
            p.ProjectStatus.Should().Be("Planned");
        }

        [TestMethod]
        public void DisasterIncident_DefaultStatus_IsPending()
        {
            var d = new DisasterIncident();
            d.DisasterIncidentStatus.Should().Be("Pending");
        }

        [TestMethod]
        public void ResourceDonation_DefaultDate_IsRecent()
        {
            var r = new ResourceDonation();
            r.ResourceDonationDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(5));
        }
    }
}   