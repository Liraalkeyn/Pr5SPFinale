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
    public class TypeOfExhibitController : ControllerBase
    {
        private readonly IDbContextFactory<Pr5SpContext> _contextFactory;

        public TypeOfExhibitController(IDbContextFactory<Pr5SpContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // GET: api/TypeOfExhibit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeOfExhibit>>> GetTypeOfExhibits()
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            return await context.TypeOfExhibits.ToListAsync();
        }

        // GET: api/TypeOfExhibit/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeOfExhibit>> GetTypeOfExhibit(int id)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            var typeOfExhibit = await context.TypeOfExhibits.FindAsync(id);

            if (typeOfExhibit == null)
            {
                return NotFound();
            }

            return typeOfExhibit;
        }

        // PUT: api/TypeOfExhibit/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeOfExhibit(int id, TypeOfExhibit typeOfExhibit)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            if (id != typeOfExhibit.TypeOfExhibitId)
            {
                return BadRequest();
            }

            context.Entry(typeOfExhibit).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeOfExhibitExists(id))
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

        // POST: api/TypeOfExhibit
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeOfExhibit>> PostTypeOfExhibit(TypeOfExhibit typeOfExhibit)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            context.TypeOfExhibits.Add(typeOfExhibit);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetTypeOfExhibit", new { id = typeOfExhibit.TypeOfExhibitId }, typeOfExhibit);
        }

        // DELETE: api/TypeOfExhibit/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeOfExhibit(int id)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            var typeOfExhibit = await context.TypeOfExhibits.FindAsync(id);
            if (typeOfExhibit == null)
            {
                return NotFound();
            }

            context.TypeOfExhibits.Remove(typeOfExhibit);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeOfExhibitExists(int id)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            return context.TypeOfExhibits.Any(e => e.TypeOfExhibitId == id);
        }
    }
}
