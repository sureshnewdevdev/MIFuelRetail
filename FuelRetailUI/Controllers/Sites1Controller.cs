using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using RetailMasters.Models;

namespace FuelRetailUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Sites1Controller : ControllerBase
    {
        private readonly AppDbContext _context;

        public Sites1Controller(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Sites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<site>>> GetSites()
        {
            return await _context.sites.Include(s => s.supplier).ToListAsync();
        }

        // GET: api/Sites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<site>> GetSite(int id)
        {
            var site = await _context.sites.Include(s => s.supplier).FirstOrDefaultAsync(s => s.siteid == id);

            if (site == null)
            {
                return NotFound();
            }

            return site;
        }

        // POST: api/Sites
        [HttpPost]
        public async Task<ActionResult<site>> CreateSite(site site)
        {
            _context.sites.Add(site);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSite), new { id = site.siteid }, site);
        }

        // PUT: api/Sites/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSite(int id, site site)
        {
            if (id != site.siteid)
            {
                return BadRequest();
            }

            _context.Entry(site).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Sites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSite(int id)
        {
            var site = await _context.sites.FindAsync(id);
            if (site == null)
            {
                return NotFound();
            }

            _context.sites.Remove(site);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SiteExists(int id)
        {
            return _context.sites.Any(e => e.siteid == id);
        }
    }
}
