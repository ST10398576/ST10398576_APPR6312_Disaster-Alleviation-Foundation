using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    [Authorize]
    public class ResourceDonationController : Controller
    {
        private readonly DRFoundationDbContext _context;
        public ResourceDonationController(DRFoundationDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var list = await _context.ResourceDonations.Include(d => d.Donor).OrderByDescending(d => d.ResourceDonationDate).ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create() => View(new ResourceDonation());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResourceDonation model)
        {
            if (!ModelState.IsValid) return View(model);

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var uid))
                model.UserID = uid;

            _context.ResourceDonations.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var donation = await _context.ResourceDonations.Include(d => d.Donor).FirstOrDefaultAsync(d => d.ResourceDonationID == id);
            if (donation == null) return NotFound();
            return View(donation);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var donation = await _context.ResourceDonations.FindAsync(id);
            if (donation == null) return NotFound();
            return View(donation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ResourceDonation donation)
        {
            if (!ModelState.IsValid) return View(donation);

            _context.Update(donation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var donation = await _context.ResourceDonations.FindAsync(id);
            if (donation == null) return NotFound();
            return View(donation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _context.ResourceDonations.FindAsync(id);
            if (donation != null)
            {
                _context.ResourceDonations.Remove(donation);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
