using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using System.Threading.Tasks;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    [Authorize]
    public class DispatchController : Controller
    {
        private readonly DRFoundationDbContext _context;

        public DispatchController(DRFoundationDbContext context)
        {
            _context = context;
        }

        // GET: Dispatch
        public async Task<IActionResult> Index()
        {
            var dispatches = await _context.Dispatches
                .Include(d => d.DisasterIncident)
                .Include(d => d.Resource)
                .ToListAsync();

            return View(dispatches);
        }

        // GET: Dispatch/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var dispatch = await _context.Dispatches
                .Include(d => d.DisasterIncident)
                .Include(d => d.Resource)
                .FirstOrDefaultAsync(m => m.DispatchID == id);

            if (dispatch == null)
                return NotFound();

            return View(dispatch);
        }

        // GET: Dispatch/Create
        public IActionResult Create()
        {
            ViewData["DisasterIncidentID"] = new SelectList(_context.DisasterIncidents, "DisasterIncidentID", "DisasterIncidentType");
            ViewData["ResourceDonationID"] = new SelectList(_context.ResourceDonations, "ResourceDonationID", "ResourceDonationType");
            return View();
        }

        // POST: Dispatch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dispatch dispatch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dispatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dispatch);
        }

        // GET: Dispatch/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var dispatch = await _context.Dispatches.FindAsync(id);
            if (dispatch == null) return NotFound();

            ViewData["DisasterIncidentID"] = new SelectList(_context.DisasterIncidents, "DisasterIncidentID", "DisasterIncidentType", dispatch.DisasterIncidentID);
            ViewData["ResourceDonationID"] = new SelectList(_context.ResourceDonations, "ResourceDonationID", "ResourceDonationType", dispatch.ResourceDonationID);
            return View(dispatch);
        }

        // POST: Dispatch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Dispatch dispatch)
        {
            if (id != dispatch.DispatchID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(dispatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dispatch);
        }

        // GET: Dispatch/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var dispatch = await _context.Dispatches
                .Include(d => d.DisasterIncident)
                .Include(d => d.Resource)
                .FirstOrDefaultAsync(m => m.DispatchID == id);

            if (dispatch == null) return NotFound();

            return View(dispatch);
        }

        // POST: Dispatch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dispatch = await _context.Dispatches.FindAsync(id);
            if (dispatch != null)
            {
                _context.Dispatches.Remove(dispatch);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
