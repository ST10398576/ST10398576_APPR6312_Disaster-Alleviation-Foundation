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

        public VolunteerController(DRFoundationDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var list = await _context.Volunteers
                .Include(v => v.User)
                .ToListAsync();

            return View(list);
        }

        [HttpGet]
        public IActionResult Register() => View(new Volunteer());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Volunteer model)
        {
            if (!ModelState.IsValid) return View(model);

            var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (claim != null && int.TryParse(claim.Value, out var uid))
                model.UserID = uid;

            _context.Volunteers.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var vol = await _context.Volunteers
                .Include(v => v.User)
                .Include(v => v.Assignments)
                .ThenInclude(pv => pv.Project)
                .FirstOrDefaultAsync(v => v.VolunteerID == id);
            if (vol == null) return NotFound();

            // pass list of available projects for assign dropdown
            ViewBag.AvailableProjects = await _context.Projects.ToListAsync();
            return View(vol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(int volunteerId, int projectId)
        {
            var assignment = new ProjectVolunteer
            {
                VolunteerID = volunteerId,
                ProjectID = projectId,
                AssignmentDate = DateTime.Now
            };
            _context.ProjectVolunteers.Add(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
