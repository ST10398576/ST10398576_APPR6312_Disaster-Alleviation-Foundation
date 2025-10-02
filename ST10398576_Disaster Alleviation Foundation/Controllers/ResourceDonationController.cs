using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using System;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    //[Authorize] // Forces login
    public class ResourceDonationController: Controller
    {
        private readonly DRFoundationDbContext _context;

        public ResourceDonationController(DRFoundationDbContext context)
        {
            _context = context;
        }

        // GET: /ResourceDonation/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResourceDonation resourceDonation)
        {
            if (ModelState.IsValid)
            {
                _context.ResourceDonations.Add(resourceDonation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Thanks");
            }

            return View(resourceDonation);
        }

        // GET: /ResourceDonation/Thanks
        public IActionResult Thanks()
        {
            return View();
        }

        // GET: /ResourceDonation/Index
        public async Task<IActionResult> Index()
        {
            var donations = await _context.ResourceDonations
                .Include(d => d.Donor)
                .ToListAsync();

            return View(donations);
        }
    }
}
