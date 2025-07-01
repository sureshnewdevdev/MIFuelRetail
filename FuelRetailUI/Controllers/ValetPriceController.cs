using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetailMasters.Models;

namespace FeulRetailUI.Controllers
{
    public class ValetPriceController : Controller
    {
        private readonly AppDbContext _context;

        public ValetPriceController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var prices = _context.valetprices
                .Include(v => v.site)
                .Include(v => v.valettype);
            return View(await prices.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var price = await _context.valetprices
                .Include(v => v.site)
                .Include(v => v.valettype)
                .FirstOrDefaultAsync(m => m.valetpriceid == id);

            if (price == null) return NotFound();

            return View(price);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("siteid,valettypeid,price,effectivedate")] valetprice valetprice)
        {
            // Remove navigation properties from validation
            ModelState.Remove("site");
            ModelState.Remove("valettype");

            if (ModelState.IsValid)
            {
                _context.Add(valetprice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Log validation errors for debugging
            LogValidationErrors();

            // Repopulate dropdowns if validation fails
            await PopulateDropdownsAsync(valetprice.siteid, valetprice.valettypeid);
            return View(valetprice);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var valetprice = await _context.valetprices.FindAsync(id);
            if (valetprice == null) return NotFound();

            await PopulateDropdownsAsync(valetprice.siteid, valetprice.valettypeid);
            return View(valetprice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("valetpriceid,siteid,valettypeid,price,effectivedate")] valetprice valetprice)
        {
            if (id != valetprice.valetpriceid) return NotFound();

            // Remove navigation properties from validation
            ModelState.Remove("site");
            ModelState.Remove("valettype");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(valetprice);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ValetPriceExistsAsync(valetprice.valetpriceid))
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
            await PopulateDropdownsAsync(valetprice.siteid, valetprice.valettypeid);
            return View(valetprice);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var valetprice = await _context.valetprices
                .Include(v => v.site)
                .Include(v => v.valettype)
                .FirstOrDefaultAsync(m => m.valetpriceid == id);

            if (valetprice == null) return NotFound();

            return View(valetprice);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var valetprice = await _context.valetprices.FindAsync(id);
            if (valetprice != null)
            {
                _context.valetprices.Remove(valetprice);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        #region Helper Methods

        /// <summary>
        /// Populates dropdown lists for sites and valet types
        /// </summary>
        /// <param name="selectedSiteId">Currently selected site ID</param>
        /// <param name="selectedValetTypeId">Currently selected valet type ID</param>
        private async Task PopulateDropdownsAsync(int? selectedSiteId = null, int? selectedValetTypeId = null)
        {
            ViewData["siteid"] = new SelectList(
                await _context.sites.ToListAsync(),
                "siteid",
                "sitecode",
                selectedSiteId);

            ViewData["valettypeid"] = new SelectList(
                await _context.valettypes.ToListAsync(),
                "valettypeid",
                "valettypename",
                selectedValetTypeId);
        }

        /// <summary>
        /// Checks if a valet price record exists
        /// </summary>
        /// <param name="id">Valet price ID</param>
        /// <returns>True if exists, false otherwise</returns>
        private async Task<bool> ValetPriceExistsAsync(int id)
        {
            return await _context.valetprices.AnyAsync(e => e.valetpriceid == id);
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
