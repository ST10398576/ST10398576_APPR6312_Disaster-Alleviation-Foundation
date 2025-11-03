using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using System;

namespace ST10398576.Tests.TestHelpers
{
    public static class InMemoryDbContextFactory
    {
        public static DRFoundationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<DRFoundationDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
                .Options;
            var ctx = new DRFoundationDbContext(options);

            // Optionally seed minimal data here if many tests use it.
            ctx.Database.EnsureCreated();
            return ctx;
        }
    }
}
