using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetailMasters.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FeulRetailUI.Controllers
{
    public class SiteController : Controller
    {
        private readonly AppDbContext _context;

        public SiteController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sites = _context.sites.Include(s => s.supplier);
            return View(await sites.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var site = await _context.sites
                .Include(s => s.supplier)
                .FirstOrDefaultAsync(m => m.siteid == id);

            if (site == null) return NotFound();

            return View(site);
        }

        public IActionResult Create()
        {
            ViewData["supplierid"] = new SelectList(_context.suppliers, "supplierid", "supdesc");
            return View(new site { isactive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("sitecode,description,supplierid,shiftstatus,isactive")] site site)
        {
            if (ModelState.IsValid)
            {
                _context.Add(site);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["supplierid"] = new SelectList(_context.suppliers, "supplierid", "supdesc", site.supplierid);
            return View(site);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var site = await _context.sites.FindAsync(id);
            if (site == null) return NotFound();

            ViewData["supplierid"] = new SelectList(_context.suppliers, "supplierid", "supdesc", site.supplierid);
            return View(site);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("siteid,sitecode,description,supplierid,shiftstatus,isactive")] site site)
        {
            if (id != site.siteid) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(site);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.sites.Any(e => e.siteid == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["supplierid"] = new SelectList(_context.suppliers, "supplierid", "supdesc", site.supplierid);
            return View(site);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var site = await _context.sites
                .Include(s => s.supplier)
                .FirstOrDefaultAsync(m => m.siteid == id);
            if (site == null) return NotFound();

            return View(site);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var site = await _context.sites.FindAsync(id);
            if (site != null)
            {
                _context.sites.Remove(site);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
