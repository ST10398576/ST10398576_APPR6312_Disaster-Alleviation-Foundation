using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using Xunit;

namespace ST10398576.Tests.IntegrationTests
{
    public class AccountIntegrationTests : IClassFixture<TestWebAppFactory>
    {
        private readonly HttpClient _client;

        public AccountIntegrationTests(TestWebAppFactory factory)
        {
            _client = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false // We test redirects manually
            });
        }

        [Fact]
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
            var registerResponse = await _client.PostAsync("/Account/Register", formData);

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
