using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using System;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    [Authorize]
    public class DisasterIncidentController: Controller
    {
        private readonly DRFoundationDbContext _context;

        public DisasterIncidentController(DRFoundationDbContext context) => _context = context;

        // GET: /DisasterIncident
        public async Task<IActionResult> Index()
        {
            var list = await _context.DisasterIncidents
                .Include(i => i.Reporter)
                .OrderByDescending(i => i.ReportDate)
                .ToListAsync();
            return View(list);
        }

        // GET: /DisasterIncident/Report (Create)
        [HttpGet]
        public IActionResult Report() => View();

        // POST: /DisasterIncident/Report
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Report(DisasterIncident model)
        {
            if (!ModelState.IsValid) return View(model);

            _context.DisasterIncidents.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var incident = await _context.DisasterIncidents
                .Include(i => i.Reporter)
                .FirstOrDefaultAsync(i => i.DisasterIncidentID == id);
            if (incident == null) return NotFound();
            return View(incident);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var incident = await _context.DisasterIncidents.FindAsync(id);
            if (incident == null) return NotFound();
            return View(incident);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DisasterIncident model)
        {
            if (id != model.DisasterIncidentID) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            _context.DisasterIncidents.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}