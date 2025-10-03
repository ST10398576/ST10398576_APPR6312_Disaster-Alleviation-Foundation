using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    [Authorize]
    public class DisasterIncidentController : Controller
    {
        private readonly DRFoundationDbContext _context;
        public DisasterIncidentController(DRFoundationDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var list = await _context.DisasterIncidents.Include(i => i.Reporter).OrderByDescending(i => i.ReportDate).ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Report() => View(new DisasterIncident());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Report(DisasterIncident model)
        {
            if (!ModelState.IsValid) return View(model);

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var uid))
                model.UserID = uid;

            _context.DisasterIncidents.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var incident = await _context.DisasterIncidents.Include(i => i.Reporter).FirstOrDefaultAsync(i => i.DisasterIncidentID == id);
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
        public async Task<IActionResult> Edit(DisasterIncident incident)
        {
            if (!ModelState.IsValid) return View(incident);

            _context.Update(incident);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var incident = await _context.DisasterIncidents.FindAsync(id);
            if (incident == null) return NotFound();
            return View(incident);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incident = await _context.DisasterIncidents.FindAsync(id);
            if (incident != null)
            {
                _context.DisasterIncidents.Remove(incident);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
