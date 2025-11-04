using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST10398576_Disaster_Alleviation_Foundation.Models;

namespace GiftOfTheGivers.Tests.IntegrationTests
{
    [TestClass]
    public class AccountIntegrationTests
    {
        private static TestWebAppFactory? _factory;
        private static HttpClient? _client;

        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            _factory = new TestWebAppFactory();
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false // We test redirects manually
            });
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _client?.Dispose();
            _factory?.Dispose();
        }

        [TestMethod]
        public async Task Register_Login_Flow_Works()
        {
            // Arrange: valid registration form data
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("FullName", "Test User"),
                new KeyValuePair<string, string>("Email", "test@example.com"),
                new KeyValuePair<string, string>("Password", "Pass123!"),
                new KeyValuePair<string, string>("ConfirmPassword", "Pass123!"),
                new KeyValuePair<string, string>("Role", "Donor")
            });

            // Act 1: POST to /Account/Register
            var registerResponse = await _client!.PostAsync("/Account/Register", formData);

            // Assert registration redirects (302)
            registerResponse.StatusCode.Should().Be(HttpStatusCode.Found);

            // Follow redirect manually to confirm route
            registerResponse.Headers.Location!.ToString().Should().Contain("/Account/Login");

            // Act 2: perform login using same credentials
            var loginForm = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Email", "test@example.com"),
                new KeyValuePair<string, string>("Password", "Pass123!")
            });

            var loginResponse = await _client.PostAsync("/Account/Login", loginForm);

            // Assert login redirects to home (302)
            loginResponse.StatusCode.Should().Be(HttpStatusCode.Found);
            loginResponse.Headers.Location!.ToString().Should().Contain("/");
        }
    }
}
