using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using System;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    public class ResourceDonationController
    {
        private readonly DRFoundationDbContext _context;

        public ResourceDonationController(DRFoundationDbContext context) => _context = context;

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ResourceDonation donation)
        {
            if (ModelState.IsValid)
            {
                _context.Donations.Add(donation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Thanks");
            }
            return View(donation);
        }

        public IActionResult Thanks() => View();
    }
}
