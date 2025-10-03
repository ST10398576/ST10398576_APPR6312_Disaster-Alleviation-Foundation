using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ST10398576_Disaster_Alleviation_Foundation.Data;
using ST10398576_Disaster_Alleviation_Foundation.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ST10398576_Disaster_Alleviation_Foundation.Controllers
{
    public class DispatchController : Controller
    {
        private readonly DRFoundationDbContext _context;

        public DispatchController(DRFoundationDbContext context)
        {
            _context = context;
        }

        // GET: Dispatch
        public async Task<IActionResult> Index()
        {
            var dispatches = _context.Dispatches
                .Include(d => d.ResourceDonation)
                .Include(d => d.Project);
            return View(await dispatches.ToListAsync());
        }

        // GET: Dispatch/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var dispatch = await _context.Dispatches
                .Include(d => d.ResourceDonation)
                .Include(d => d.Project)
                .FirstOrDefaultAsync(m => m.DispatchID == id);

            if (dispatch == null) return NotFound();

            return View(dispatch);
        }

        // GET: Dispatch/Create
        public IActionResult Create()
        {
            ViewBag.Donations = _context.ResourceDonations.ToList();
            ViewBag.Projects = _context.Projects.ToList();
            return View();
        }

        // POST: Dispatch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DispatchID,DispatchDate,QuantityDispatched,ResourceDonationID,ProjectID")] Dispatch dispatch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dispatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Donations = _context.ResourceDonations.ToList();
            ViewBag.Projects = _context.Projects.ToList();
            return View(dispatch);
        }

        // GET: Dispatch/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var dispatch = await _context.Dispatches.FindAsync(id);
            if (dispatch == null) return NotFound();

            ViewBag.Donations = _context.ResourceDonations.ToList();
            ViewBag.Projects = _context.Projects.ToList();
            return View(dispatch);
        }

        // POST: Dispatch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DispatchID,DispatchDate,QuantityDispatched,ResourceDonationID,ProjectID")] Dispatch dispatch)
        {
            if (id != dispatch.DispatchID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dispatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DispatchExists(dispatch.DispatchID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Donations = _context.ResourceDonations.ToList();
            ViewBag.Projects = _context.Projects.ToList();
            return View(dispatch);
        }

        // GET: Dispatch/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var dispatch = await _context.Dispatches
                .Include(d => d.ResourceDonation)
                .Include(d => d.Project)
                .FirstOrDefaultAsync(m => m.DispatchID == id);

            if (dispatch == null) return NotFound();

            return View(dispatch);
        }

        // POST: Dispatch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dispatch = await _context.Dispatches.FindAsync(id);
            if (dispatch != null)
            {
                _context.Dispatches.Remove(dispatch);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DispatchExists(int id)
        {
            return _context.Dispatches.Any(e => e.DispatchID == id);
        }
    }
}
