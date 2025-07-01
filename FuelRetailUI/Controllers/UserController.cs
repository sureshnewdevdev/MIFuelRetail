using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetailMasters.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FeulRetailUI.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = _context.users.Include(u => u.role);
            return View(await users.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.users
                .Include(u => u.role)
                .FirstOrDefaultAsync(m => m.userid == id);
            if (user == null) return NotFound();

            return View(user);
        }

        public IActionResult Create()
        {
            ViewData["roleid"] = new SelectList(_context.roles, "roleid", "rolename");
            return View(new user { isactive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("username,passwordhash,roleid,email,isactive")] user user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["roleid"] = new SelectList(_context.roles, "roleid", "rolename", user.roleid);
            return View(user);
        }

        public async Task<IActionResult> Edit(int? id)
        {   
            if (id == null) return NotFound();

            var user = await _context.users.FindAsync(id);
            if (user == null) return NotFound();

            ViewData["roleid"] = new SelectList(_context.roles, "roleid", "rolename", user.roleid);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("userid,username,passwordhash,roleid,email,isactive")] user user)
        {
            if (id != user.userid) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.users.Any(e => e.userid == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["roleid"] = new SelectList(_context.roles, "roleid", "rolename", user.roleid);
            return View(user);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.users
                .Include(u => u.role)
                .FirstOrDefaultAsync(m => m.userid == id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user != null)
            {
                _context.users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
