using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with Azure connection
var connectionString = builder.Configuration.GetConnectionString("AzureSqlConnection");

builder.Services.AddDbContext<DRFoundationDbContext>(options =>
    options.UseSqlServer(connectionString));


// Add ASP.NET Identity
builder.Services.AddIdentity<AppUser, UserRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<DRFoundationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.LogoutPath = "/Account/Logout";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<UserRole>>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var config = services.GetRequiredService<IConfiguration>();

    Task.Run(async () =>
    {
        var roles = new[] { "Admin", "Volunteer", "Donor" };
        foreach (var r in roles)
        {
            if (!await roleManager.RoleExistsAsync(r))
                await roleManager.CreateAsync(new UserRole { Name = r });
        }

        // Optional admin seed — set these in user secrets or Azure App Settings:
        // SeedAdmin:Email  and SeedAdmin:Password
        var adminEmail = config["SeedAdmin:Email"];
        var adminPassword = config["SeedAdmin:Password"];
        if (!string.IsNullOrWhiteSpace(adminEmail) && !string.IsNullOrWhiteSpace(adminPassword))
        {
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new AppUser { UserName = adminEmail, Email = adminEmail, FullName = "Administrator" };
                var res = await userManager.CreateAsync(adminUser, adminPassword);
                if (res.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }).GetAwaiter().GetResult();
}

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Default route: start the app at the registration page.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();