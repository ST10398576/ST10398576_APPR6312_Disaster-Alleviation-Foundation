using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using System;
using System.Linq;

namespace GiftOfTheGivers.Tests.IntegrationTests
{
    public class TestWebAppFactory : WebApplicationFactory<Program>, IDisposable
    {
        private readonly SqliteConnection _connection;

        public TestWebAppFactory()
        {
            // Shared in-memory SQLite connection
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing"); // ensure "Testing" env so Program.cs skips Migrate()

            builder.ConfigureServices(services =>
            {
                // Remove the production DB context
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<DRFoundationDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                // Add a new SQLite in-memory context
                services.AddDbContext<DRFoundationDbContext>(options =>
                    options.UseSqlite(_connection));

                // Build the provider
                var sp = services.BuildServiceProvider();

                // Create the database schema
                using var scope = sp.CreateScope();
                var ctx = scope.ServiceProvider.GetRequiredService<DRFoundationDbContext>();
                ctx.Database.EnsureCreated(); // Create tables, no migration scripts
            });
        }

        public new void Dispose()
        {
            base.Dispose();
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
