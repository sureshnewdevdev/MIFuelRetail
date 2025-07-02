using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailMasters.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FeulRetailUI.Controllers
{
    public class ValetTypeController : Controller
    {
        private readonly AppDbContext _context;

        public ValetTypeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.valettypes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var valettype = await _context.valettypes
                .FirstOrDefaultAsync(m => m.Valettypeid == id);
            if (valettype == null) return NotFound();

            return View(valettype);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("valettypename")] Valettype valettype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(valettype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(valettype);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var valettype = await _context.valettypes.FindAsync(id);
            if (valettype == null) return NotFound();

            return View(valettype);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("valettypeid,valettypename")] Valettype valettype)
        {
            if (id != valettype.Valettypeid) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(valettype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.valettypes.Any(e => e.Valettypeid == id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(valettype);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var valettype = await _context.valettypes
                .FirstOrDefaultAsync(m => m.Valettypeid == id);
            if (valettype == null) return NotFound();

            return View(valettype);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var valettype = await _context.valettypes.FindAsync(id);
            if (valettype != null)
            {
                _context.valettypes.Remove(valettype);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
