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
    public class ResponsibleEmployeeController : ControllerBase
    {
        private readonly IDbContextFactory<Pr5SpContext> _contextFactory;

        public ResponsibleEmployeeController(IDbContextFactory<Pr5SpContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // GET: api/ResponsibleEmployee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponsibleEmployee>>> GetResponsibleEmployees()
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            return await context.ResponsibleEmployees.ToListAsync();
        }

        // GET: api/ResponsibleEmployee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponsibleEmployee>> GetResponsibleEmployee(int id)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            var responsibleEmployee = await context.ResponsibleEmployees.FindAsync(id);

            if (responsibleEmployee == null)
            {
                return NotFound();
            }

            return responsibleEmployee;
        }

        // PUT: api/ResponsibleEmployee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResponsibleEmployee(int id, ResponsibleEmployee responsibleEmployee)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            if (id != responsibleEmployee.ResponsibleEmployeeId)
            {
                return BadRequest();
            }

            context.Entry(responsibleEmployee).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponsibleEmployeeExists(id))
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

        // POST: api/ResponsibleEmployee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResponsibleEmployee>> PostResponsibleEmployee(ResponsibleEmployee responsibleEmployee)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            context.ResponsibleEmployees.Add(responsibleEmployee);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetResponsibleEmployee", new { id = responsibleEmployee.ResponsibleEmployeeId }, responsibleEmployee);
        }

        // DELETE: api/ResponsibleEmployee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResponsibleEmployee(int id)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            var responsibleEmployee = await context.ResponsibleEmployees.FindAsync(id);
            if (responsibleEmployee == null)
            {
                return NotFound();
            }

            context.ResponsibleEmployees.Remove(responsibleEmployee);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResponsibleEmployeeExists(int id)
        {
            Pr5SpContext context = _contextFactory.CreateDbContext();
            return context.ResponsibleEmployees.Any(e => e.ResponsibleEmployeeId == id);
        }
    }
}
