using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetailMasters.Models;

namespace FeulRetailUI.Controllers
{
    public class PriceChangeController : Controller
    {
        private readonly AppDbContext _context;

        public PriceChangeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PriceChange
        public async Task<IActionResult> Index()
        {
            var priceChanges = _context.pricechanges
                .Include(p => p.Fueltype)
                .OrderByDescending(p => p.Changedate);
            return View(await priceChanges.ToListAsync());
        }

        // GET: PriceChange/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var priceChange = await _context.pricechanges
                .Include(p => p.Fueltype)
                .FirstOrDefaultAsync(m => m.Changeid == id);

            if (priceChange == null) return NotFound();

            return View(priceChange);
        }

        // GET: PriceChange/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View();
        }

        // POST: PriceChange/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("fueltypeid,oldprice,newprice,changedate")] Pricechange pricechange)
        {
            // Remove navigation properties from validation
            ModelState.Remove("fueltype");

            // Set default change date if not provided
            if (pricechange.Changedate == null)
            {
                pricechange.Changedate = DateTime.Now;
            }

            if (ModelState.IsValid)
            {
                _context.Add(pricechange);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Log validation errors for debugging
            LogValidationErrors();

            // Repopulate dropdowns if validation fails
            await PopulateDropdownsAsync(pricechange.Fueltypeid);
            return View(pricechange);
        }

        // GET: PriceChange/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pricechange = await _context.pricechanges.FindAsync(id);
            if (pricechange == null) return NotFound();

            await PopulateDropdownsAsync(pricechange.Fueltypeid);
            return View(pricechange);
        }

        // POST: PriceChange/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("changeid,fueltypeid,oldprice,newprice,changedate")] Pricechange pricechange)
        {
            if (id != pricechange.Changeid) return NotFound();

            // Remove navigation properties from validation
            ModelState.Remove("fueltype");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pricechange);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PriceChangeExistsAsync(pricechange.Changeid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Log validation errors for debugging
            LogValidationErrors();

            // Repopulate dropdowns if validation fails
            await PopulateDropdownsAsync(pricechange.Fueltypeid);
            return View(pricechange);
        }

        // GET: PriceChange/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var pricechange = await _context.pricechanges
                .Include(p => p.Fueltype)
                .FirstOrDefaultAsync(m => m.Changeid == id);

            if (pricechange == null) return NotFound();

            return View(pricechange);
        }

        // POST: PriceChange/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pricechange = await _context.pricechanges.FindAsync(id);
            if (pricechange != null)
            {
                _context.pricechanges.Remove(pricechange);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        #region Helper Methods

        /// <summary>
        /// Populates dropdown lists for fuel types
        /// </summary>
        /// <param name="selectedFuelTypeId">Currently selected fuel type ID</param>
        private async Task PopulateDropdownsAsync(int? selectedFuelTypeId = null)
        {
            ViewData["fueltypeid"] = new SelectList(
                await _context.fueltypes.ToListAsync(),
                "fueltypeid",
                "fueltypename",
                selectedFuelTypeId);
        }

        /// <summary>
        /// Checks if a price change record exists
        /// </summary>
        /// <param name="id">Price change ID</param>
        /// <returns>True if exists, false otherwise</returns>
        private async Task<bool> PriceChangeExistsAsync(int id)
        {
            return await _context.pricechanges.AnyAsync(e => e.Changeid == id);
        }

        /// <summary>
        /// Logs validation errors to console for debugging
        /// </summary>
        private void LogValidationErrors()
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation Error: {modelError.ErrorMessage}");
                }
            }
        }

        #endregion
    }
}
