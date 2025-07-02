using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailMasters.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FeulRetailUI.Controllers
{
    public class FuelTypeController : Controller
    {
        private readonly AppDbContext _context;

        public FuelTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: FuelType
        public async Task<IActionResult> Index()
        {
            return View(await _context.fueltypes.ToListAsync());
        }

        // GET: FuelType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var fueltype = await _context.fueltypes
                .FirstOrDefaultAsync(m => m.Fueltypeid == id);
            if (fueltype == null) return NotFound();

            return View(fueltype);
        }

        // GET: FuelType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FuelType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Fueltypename,Description")] Fueltype fueltype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fueltype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fueltype);
        }

        // GET: FuelType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var fueltype = await _context.fueltypes.FindAsync(id);
            if (fueltype == null) return NotFound();

            return View(fueltype);
        }

        // POST: FuelType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Fueltypeid,Fueltypename,Description")] Fueltype fueltype)
        {
            if (id != fueltype.Fueltypeid) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fueltype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.fueltypes.Any(e => e.Fueltypeid == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fueltype);
        }

        // GET: FuelType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var fueltype = await _context.fueltypes
                .FirstOrDefaultAsync(m => m.Fueltypeid == id);
            if (fueltype == null) return NotFound();

            return View(fueltype);
        }

        // POST: FuelType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fueltype = await _context.fueltypes.FindAsync(id);
            if (fueltype != null)
            {
                _context.fueltypes.Remove(fueltype);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
