using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using System;
using System.Threading.Tasks;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly DRFoundationDbContext _context;

        public ProjectController(DRFoundationDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var list = await _context.Projects
                .Include(p => p.Volunteers)
                .ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create() => View();

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
                .Include(p => p.Dispatches)
                .FirstOrDefaultAsync(p => p.ProjectID == id);
            if (project == null) return NotFound();
            return View(project);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project model)
        {
            if (id != model.ProjectID) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            _context.Projects.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}