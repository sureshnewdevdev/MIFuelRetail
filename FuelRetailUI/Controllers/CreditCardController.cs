using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailMasters.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FeulRetailUI.Controllers
{
    public class CreditCardController : Controller
    {
        private readonly AppDbContext _context;

        public CreditCardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.creditcards.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var creditcard = await _context.creditcards
                .FirstOrDefaultAsync(m => m.cardid == id);
            if (creditcard == null) return NotFound();

            return View(creditcard);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("cardname,cardtype")] creditcard creditcard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creditcard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(creditcard);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var creditcard = await _context.creditcards.FindAsync(id);
            if (creditcard == null) return NotFound();

            return View(creditcard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("cardid,cardname,cardtype")] creditcard creditcard)
        {
            if (id != creditcard.cardid) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creditcard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.creditcards.Any(e => e.cardid == id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(creditcard);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var creditcard = await _context.creditcards
                .FirstOrDefaultAsync(m => m.cardid == id);
            if (creditcard == null) return NotFound();

            return View(creditcard);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var creditcard = await _context.creditcards.FindAsync(id);
            if (creditcard != null)
            {
                _context.creditcards.Remove(creditcard);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
