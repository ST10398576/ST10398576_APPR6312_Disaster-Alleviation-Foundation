using Microsoft.AspNetCore.Mvc;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using Microsoft.EntityFrameworkCore;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly DRFoundationDbContext _context;

        public VolunteerController(DRFoundationDbContext context)
        {
            _context = context;
        }

        // GET: /Volunteer
        public IActionResult Index()
        {
            var volunteers = _context.Volunteers.ToList();
            return View(volunteers);
        }

        // GET: /Volunteer/Details/{id}
        public IActionResult Details(int id)
        {
            var volunteer = _context.Volunteers
                .Include(v => v.User)
                .FirstOrDefault(v => v.VolunteerID == id);

            if (volunteer == null)
                return NotFound();

            return View(volunteer);
        }

        // GET: /Volunteer/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Volunteer/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                _context.Volunteers.Add(volunteer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(volunteer);
        }
    }
}
