using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    [Authorize]
    public class VolunteerController : Controller
    {
        private readonly DRFoundationDbContext _context;

        public VolunteerController(DRFoundationDbContext context)
        {
            _context = context;
        }

        // GET: Volunteer
        public async Task<IActionResult> Index()
        {
            var volunteers = await _context.Volunteers
                .Include(v => v.User)
                .ToListAsync();

            return View(volunteers);
        }

        // GET: Volunteer/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var volunteer = await _context.Volunteers
                .Include(v => v.User)
                .FirstOrDefaultAsync(v => v.VolunteerID == id);

            if (volunteer == null) return NotFound();
            return View(volunteer);
        }

        // GET: Volunteer/Register
        public IActionResult Register() => View();

        // POST: Volunteer/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Volunteer volunteer)
        {
            if (!ModelState.IsValid) return View(volunteer);

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var uid))
                volunteer.UserID = uid;

            _context.Volunteers.Add(volunteer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Volunteer/Edit/
        public async Task<IActionResult> Edit(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer == null) return NotFound();
            return View(volunteer);
        }

        // POST: Volunteer/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Volunteer volunteer)
        {
            if (!ModelState.IsValid) return View(volunteer);
                
            _context.Update(volunteer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Volunteer/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var volunteer = await _context.Volunteers.FirstOrDefaultAsync(v => v.VolunteerID == id);
            if (volunteer == null) return NotFound();
            return View(volunteer);
        }

        // POST: Volunteer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer != null)
            {
                _context.Volunteers.Remove(volunteer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
