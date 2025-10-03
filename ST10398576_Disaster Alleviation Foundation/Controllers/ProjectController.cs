using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly DRFoundationDbContext _context;
        public ProjectController(DRFoundationDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var list = await _context.Projects.ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create() => View(new Project());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project model)
        {
            if (!ModelState.IsValid) return View(model);
            _context.Projects.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Volunteers)
                .ThenInclude(pv => pv.Volunteer)
                .ThenInclude(v => v.User)
                .FirstOrDefaultAsync(p => p.ProjectID == id);
            if (project == null) return NotFound();

            // pass all volunteers for assign dropdown in view
            ViewBag.AvailableVolunteers = await _context.Volunteers.Include(v => v.User).ToListAsync();
            return View(project);
        }
    }
}
