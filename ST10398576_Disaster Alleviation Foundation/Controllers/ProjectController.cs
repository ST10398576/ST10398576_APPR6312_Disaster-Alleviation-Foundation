using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using System;
using System.Threading.Tasks;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    [Authorize] // Forces login
    public class ProjectController : Controller
    {
        private readonly DRFoundationDbContext _context;

        public ProjectController(DRFoundationDbContext context) => _context = context;

        public async Task<IActionResult> Index() => View(await _context.Projects.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        public async Task<IActionResult> Details(int id)
        {
            var project = await _context.Projects.Include(p => p.Volunteers).FirstOrDefaultAsync(p => p.ProjectID == id);
            if (project == null) return NotFound();
            return View(project);
        }
    }
}
