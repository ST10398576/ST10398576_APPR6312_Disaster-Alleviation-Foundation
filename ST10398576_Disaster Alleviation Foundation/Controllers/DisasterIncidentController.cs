using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using System;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    public class DisasterIncidentController
    {
        private readonly DRFoundationDbContext _context;

        public DisasterIncidentController(DRFoundationDbContext context) => _context = context;

        [HttpGet]
        public IActionResult Report() => View();

        [HttpPost]
        public async Task<IActionResult> Report(DisasterIncident incident)
        {
            if (ModelState.IsValid)
            {
                _context.Incidents.Add(incident);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(incident);
        }

        public async Task<IActionResult> List() =>
            View(await _context.Incidents.Include(i => i.Reporter).ToListAsync());
        
    }
}
