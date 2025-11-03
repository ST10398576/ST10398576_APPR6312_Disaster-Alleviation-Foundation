using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    public class DisasterIncidentController : Controller
    {
        private readonly DRFoundationDbContext _context;

        public DisasterIncidentController(DRFoundationDbContext context)
        {
            _context = context;
        }

        // GET: /DisasterIncident
        public IActionResult Index()
        {
            var incidents = _context.DisasterIncidents
                .Include(d => d.Reporter)
                .ToList();

            return View(incidents);
        }

        // GET: /DisasterIncident/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /DisasterIncident/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DisasterIncident model)
        {
            if (ModelState.IsValid)
            {
                _context.DisasterIncidents.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: /DisasterIncident/Details/{id}
        public IActionResult Details(int id)
        {
            var incident = _context.DisasterIncidents
                .Include(d => d.Reporter)
                .FirstOrDefault(d => d.DisasterIncidentID == id);

            if (incident == null)
                return NotFound();

            return View(incident);
        }
    }
}
