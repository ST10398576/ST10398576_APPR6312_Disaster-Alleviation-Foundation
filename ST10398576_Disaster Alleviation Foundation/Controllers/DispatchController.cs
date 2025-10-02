using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using System;
namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    [Authorize] // Forces login
    public class DispatchController : Controller
    {
        private readonly DRFoundationDbContext _context;
        public DispatchController(DRFoundationDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Projects = await _context.Projects.ToListAsync();
            ViewBag.Resources = await _context.ProjectResources.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Dispatch dispatch)
        {
            if (ModelState.IsValid)
            {
                _context.Dispatches.Add(dispatch);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dispatch);
        }

        public async Task<IActionResult> Index() =>
            View(await _context.Dispatches
                .Include(d => d.Project)
                .Include(d => d.Resource)
                .ToListAsync());
    }
}
