using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using System;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    
    public class ResourceDonationController: Controller
    {
        private readonly DRFoundationDbContext _context;

        public ResourceDonationController(DRFoundationDbContext context) => _context = context;

        // GET: /ResourceDonation
        public async Task<IActionResult> Index()
        {
            var list = await _context.ResourceDonations
                .Include(d => d.Donor)
                .OrderByDescending(d => d.ResourceDonationDate)
                .ToListAsync();
            return View(list);
        }

        // GET: /ResourceDonation/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ResourceDonation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResourceDonation model)
        {
            if (!ModelState.IsValid) return View(model);

            _context.ResourceDonations.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Thanks));
        }

        public IActionResult Thanks() => View();


        // GET: /ResourceDonation/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var donation = await _context.ResourceDonations
                .Include(d => d.Donor)
                .FirstOrDefaultAsync(d => d.ResourceDonationID == id);
            if (donation == null) return NotFound();
            return View(donation);
        }

        // GET: /ResourceDonation/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var donation = await _context.ResourceDonations.FindAsync(id);
            if (donation == null) return NotFound();
            return View(donation);
        }

        // POST: /ResourceDonation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResourceDonation model)
        {
            if (id != model.ResourceDonationID) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            _context.ResourceDonations.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}