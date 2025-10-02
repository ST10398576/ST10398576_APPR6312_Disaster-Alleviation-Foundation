using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with Azure connection
builder.Services.AddDbContext<DRFoundationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnection")));

// Identity setup
builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false) // disable email confirm for now
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DRFoundationDbContext>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Identity middlewares
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();