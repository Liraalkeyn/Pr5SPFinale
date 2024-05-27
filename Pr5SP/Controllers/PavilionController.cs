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
    public class PavilionController : ControllerBase
    {
        private readonly IDbContextFactory<Pr5SpContext> _contextFactory;

        public PavilionController(IDbContextFactory<Pr5SpContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // GET: api/Pavilion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pavilion>>> GetPavilions()
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            
            return await context.Pavilions.ToListAsync();
        }

        // GET: api/Pavilion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pavilion>> GetPavilion(int id)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            var pavilion = await context.Pavilions.FindAsync(id);

            if (pavilion == null)
            {
                return NotFound();
            }

            return pavilion;
        }

        // PUT: api/Pavilion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPavilion(int id, Pavilion pavilion)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            if (id != pavilion.PavilionId)
            {
                return BadRequest();
            }

            context.Entry(pavilion).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PavilionExists(id))
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

        // POST: api/Pavilion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pavilion>> PostPavilion(Pavilion pavilion)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            context.Pavilions.Add(pavilion);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetPavilion", new { id = pavilion.PavilionId }, pavilion);
        }

        // DELETE: api/Pavilion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePavilion(int id)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            var pavilion = await context.Pavilions.FindAsync(id);
            if (pavilion == null)
            {
                return NotFound();
            }

            context.Pavilions.Remove(pavilion);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool PavilionExists(int id)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            return context.Pavilions.Any(e => e.PavilionId == id);
        }
    }
}
