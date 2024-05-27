using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pr5SP.Context;
using Pr5SP.Models;

namespace Pr5SP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExhibitController : ControllerBase
    {
        private readonly IDbContextFactory<Pr5SpContext> _contextFactory;

        public ExhibitController(IDbContextFactory<Pr5SpContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // GET: api/Exhibit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exhibit>>> GetExhibits()
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            
            return await context.Exhibits.ToListAsync();
        }

        // GET: api/Exhibit/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exhibit>> GetExhibit(int id)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            var exhibit = await context.Exhibits.FindAsync(id);

            if (exhibit == null)
            {
                return NotFound();
            }

            return exhibit;
        }

        // PUT: api/Exhibit/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExhibit(int id, Exhibit exhibit)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            if (id != exhibit.ExhibitId)
            {
                return BadRequest();
            }

            context.Entry(exhibit).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExhibitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Exhibit
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exhibit>> PostExhibit(Exhibit exhibit)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            context.Exhibits.Add(exhibit);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetExhibit", new { id = exhibit.ExhibitId }, exhibit);
        }

        // DELETE: api/Exhibit/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExhibit(int id)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            var exhibit = await context.Exhibits.FindAsync(id);
            if (exhibit == null)
            {
                return NotFound();
            }

            context.Exhibits.Remove(exhibit);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExhibitExists(int id)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            return context.Exhibits.Any(e => e.ExhibitId == id);
        }
    }
}
