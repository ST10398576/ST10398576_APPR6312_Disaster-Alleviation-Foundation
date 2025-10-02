using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using System;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    [Authorize] // Forces login
    public class VolunteerController : Controller
    {
        private readonly DRFoundationDbContext _context;

        public VolunteerController(DRFoundationDbContext context) => _context = context;

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                _context.Volunteers.Add(volunteer);
                await _context.SaveChangesAsync();
                return RedirectToAction("BrowseProjects");
            }
            return View(volunteer);
        }

        [HttpGet]
        public async Task<IActionResult> BrowseProjects()
        {
            var projects = await _context.Projects.ToListAsync();
            return View(projects);
        }

        [HttpPost]
        public async Task<IActionResult> Assign(int volunteerId, int projectId)
        {
            var assignment = new ProjectVolunteer
            {
                VolunteerID = volunteerId,
                ProjectID = projectId
            };
            _context.ProjectVolunteers.Add(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Assignments");
        }

        [HttpGet]
        public async Task<IActionResult> Assignments()
        {
            var assignments = await _context.ProjectVolunteers
                .Include(pv => pv.Project)
                .Include(pv => pv.Volunteer)
                .ToListAsync();
            return View(assignments);
        }
    }
}
