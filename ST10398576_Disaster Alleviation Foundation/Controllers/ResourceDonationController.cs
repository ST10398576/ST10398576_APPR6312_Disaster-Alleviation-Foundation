using Microsoft.AspNetCore.Mvc;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    public class ResourceDonationController : Controller
    {
        private readonly DRFoundationDbContext _context;

        public ResourceDonationController(DRFoundationDbContext context)
        {
            _context = context;
        }

        // GET: /ResourceDonation
        public IActionResult Index()
        {
            var donations = _context.ResourceDonations.ToList();
            return View(donations);
        }

        // GET: /ResourceDonation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ResourceDonation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ResourceDonation model)
        {
            if (ModelState.IsValid)
            {
                _context.ResourceDonations.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: /ResourceDonation/Details/{id}
        public IActionResult Details(int id)
        {
            var donation = _context.ResourceDonations.Find(id);
            if (donation == null)
                return NotFound();

            return View(donation);
        }
    }
}
