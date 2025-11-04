using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GiftOfTheGivers.Tests.IntegrationTests
{
    [TestClass]
    public class ControllersIntegrationTests
    {
        private static TestWebAppFactory? _factory;
        private static HttpClient? _client;

        [ClassInitialize]
        public static void ClassInit(TestContext _)
        {
            _factory = new TestWebAppFactory();
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _client?.Dispose();
            _factory?.Dispose();
        }

        [TestMethod]
        public async Task Get_DisasterIncident_Index_ReturnsOk()
        {
            var resp = await _client!.GetAsync("/DisasterIncident");
            resp.EnsureSuccessStatusCode();
            resp.StatusCode.Should().Be(HttpStatusCode.OK);
            var html = await resp.Content.ReadAsStringAsync();
            html.Should().Contain("<");
        }

        [TestMethod]
        public async Task Get_ResourceDonation_Index_ReturnsOk()
        {
            var resp = await _client!.GetAsync("/ResourceDonation");
            resp.EnsureSuccessStatusCode();
            resp.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task Get_Volunteer_Index_ReturnsOk()
        {
            var resp = await _client!.GetAsync("/Volunteer");
            resp.EnsureSuccessStatusCode();
            resp.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task Get_Project_Index_Unauthenticated_RedirectsToLoginOrRegister()
        {
            var resp = await _client!.GetAsync("/Project");

            // should be a redirect for unauthenticated user
            resp.StatusCode.Should().Be(HttpStatusCode.Found);

            var location = resp.Headers.Location?.ToString() ?? string.Empty;
            location.Should().NotBeNullOrEmpty("unauthenticated requests must redirect to an auth page");

            var lower = location.ToLowerInvariant();
            // Accept either a login or register redirect (app sets LoginPath = "/Account/Register")
            (lower.Contains("/login") || lower.Contains("/register")).Should().BeTrue("unauthenticated requests must redirect to a login or register page");
        }
    }
}