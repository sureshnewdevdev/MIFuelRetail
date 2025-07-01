using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetailMasters.Models;

namespace FuelRetailUI.Controllers
{
    public class sitesController : Controller
    {
        private readonly AppDbContext _context;

        public sitesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: sites
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.sites.Include(s => s.supplier);
            return View(await appDbContext.ToListAsync());
        }

        // GET: sites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.sites
                .Include(s => s.supplier)
                .FirstOrDefaultAsync(m => m.siteid == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }

        // GET: sites/Create
        public IActionResult Create()
        {
            ViewData["supplierid"] = new SelectList(_context.suppliers, "supplierid", "supdesc");
            return View();
        }

        // POST: sites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("siteid,sitecode,description,supplierid,shiftstatus,isactive")] site site)
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

        // GET: sites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.sites.FindAsync(id);
            if (site == null)
            {
                return NotFound();
            }
            ViewData["supplierid"] = new SelectList(_context.suppliers, "supplierid", "supdesc", site.supplierid);
            return View(site);
        }

        // POST: sites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("siteid,sitecode,description,supplierid,shiftstatus,isactive")] site site)
        {
            if (id != site.siteid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(site);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!siteExists(site.siteid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["supplierid"] = new SelectList(_context.suppliers, "supplierid", "supdesc", site.supplierid);
            return View(site);
        }

        // GET: sites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.sites
                .Include(s => s.supplier)
                .FirstOrDefaultAsync(m => m.siteid == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }

        // POST: sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var site = await _context.sites.FindAsync(id);
            if (site != null)
            {
                _context.sites.Remove(site);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool siteExists(int id)
        {
            return _context.sites.Any(e => e.siteid == id);
        }
    }
}
