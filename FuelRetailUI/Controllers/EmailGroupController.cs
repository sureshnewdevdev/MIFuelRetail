using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailMasters.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FeulRetailUI.Controllers
{
    public class EmailGroupController : Controller
    {
        private readonly AppDbContext _context;

        public EmailGroupController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.emailgroups.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var emailgroup = await _context.emailgroups
                .FirstOrDefaultAsync(m => m.Groupid == id);
            if (emailgroup == null) return NotFound();

            return View(emailgroup);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("groupname")] Emailgroup emailgroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emailgroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emailgroup);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var emailgroup = await _context.emailgroups.FindAsync(id);
            if (emailgroup == null) return NotFound();

            return View(emailgroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("groupid,groupname")] Emailgroup emailgroup)
        {
            if (id != emailgroup.Groupid) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emailgroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.emailgroups.Any(e => e.Groupid == id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(emailgroup);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var emailgroup = await _context.emailgroups
                .FirstOrDefaultAsync(m => m.Groupid == id);
            if (emailgroup == null) return NotFound();

            return View(emailgroup);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emailgroup = await _context.emailgroups.FindAsync(id);
            if (emailgroup != null)
            {
                _context.emailgroups.Remove(emailgroup);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
