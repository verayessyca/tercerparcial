using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerParcial.Data;
using PrimerParcial.Models;
using Microsoft.AspNetCore.Authorization;

namespace PrimerParcial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CiudadesController : ControllerBase
    {
        private readonly PrimerParcialContext _context;

        public CiudadesController(PrimerParcialContext context)
        {
            _context = context;
        }

        // GET: api/Ciudades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ciudad>>> GetCiudad()
        {
          if (_context.Ciudades == null)
          {
              return NotFound();
          }
            return await _context.Ciudades.ToListAsync();
        }

        // GET: api/Ciudades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ciudad>> GetCiudad(int id)
        {
          if (_context.Ciudades == null)
          {
              return NotFound();
          }
            var ciudad = await _context.Ciudades.FindAsync(id);

            if (ciudad == null)
            {
                return NotFound();
            }

            return ciudad;
        }

        // PUT: api/Ciudades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCiudad(int id, Ciudad ciudad)
        {
            if (id != ciudad.Id)
            {
                return BadRequest();
            }

            _context.Entry(ciudad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CiudadExists(id))
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

        // POST: api/Ciudades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ciudad>> PostCiudad(Ciudad ciudad)
        {
          if (_context.Ciudades == null)
          {
              return Problem("Entity set 'PrimerParcialContext.Ciudad'  is null.");
          }
            _context.Ciudades.Add(ciudad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCiudad", new { id = ciudad.Id }, ciudad);
        }

        // DELETE: api/Ciudades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCiudad(int id)
        {
            if (_context.Ciudades == null)
            {
                return NotFound();
            }
            var ciudad = await _context.Ciudades.FindAsync(id);
            if (ciudad == null)
            {
                return NotFound();
            }

            _context.Ciudades.Remove(ciudad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CiudadExists(int id)
        {
            return (_context.Ciudades?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
