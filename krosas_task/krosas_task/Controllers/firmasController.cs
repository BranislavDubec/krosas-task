using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using krosas_task.Models;

namespace krosas_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class firmasController : ControllerBase
    {
        private readonly TokenContext _context;

        public firmasController(TokenContext context)
        {
            _context = context;
        }

        // GET: api/firmas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<firma>>> Getfirma()
        {
            return await _context.firma.ToListAsync();
        }

        // GET: api/firmas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<firma>> Getfirma(long id)
        {
            var firma = await _context.firma.FindAsync(id);

            if (firma == null)
            {
                return NotFound();
            }

            return firma;
        }

        // PUT: api/firmas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<firma>> Putfirma(long id, firma firma)
        {
            if (id != firma.id)
            {
                return BadRequest();
            }

            var fir = _context.firma.FirstOrDefault(s => s.id.Equals(id));
            if (fir == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(firma.nazov))
            {
                firma.nazov = fir.nazov;
            }
            if (String.IsNullOrEmpty(firma.kod))
            {
                firma.kod = fir.kod;
            }
            if (firma.veduciID == 0)
            {
                firma.veduciID = fir.veduciID;
            }

            if (!zamestnanecExists(firma.veduciID))
            {
                ModelState.AddModelError(nameof(firma.veduciID), "Employee does not exist.");
                return ValidationProblem(ModelState);
            }
            _context.Remove(fir);
            _context.Update(firma);
            _context.SaveChanges();
            return firma;
        }

        // POST: api/firmas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<firma>> Postfirma(firma firma)
        {
            if (String.IsNullOrEmpty(firma.nazov))
            {
                ModelState.AddModelError(nameof(firma.nazov), "Nazov is empty.");
                return ValidationProblem(ModelState);
            }
            if (String.IsNullOrEmpty(firma.kod))
            {
                ModelState.AddModelError(nameof(firma.kod), "Kod is empty.");
                return ValidationProblem(ModelState);
            }
            
            if (firmaExists(firma.id) || firma.id < 1)
            {
                ModelState.AddModelError(nameof(firma.id), "Id already exists or is negative.");
                return ValidationProblem(ModelState);
            }

            if (!zamestnanecExists(firma.veduciID)) {
                ModelState.AddModelError(nameof(firma.veduciID), "Employee does not exist.");
                return ValidationProblem(ModelState);
            }
            try
            {

                _context.firma.Add(firma);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }

            return CreatedAtAction("Getfirma", new { id = firma.id }, firma);
        }

        // DELETE: api/firmas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletefirma(long id)
        {
            var firma = await _context.firma.FindAsync(id);
            if (firma == null)
            {
                return NotFound();
            }

            try
            {
                _context.firma.Remove(firma);
                await _context.SaveChangesAsync();
            }
            catch 
            {
                ModelState.AddModelError(nameof(firma.id), "Firma has divisions.");
                return ValidationProblem(ModelState);
            }
            

            return NoContent();
        }

        private bool firmaExists(long id)
        {
            return _context.firma.Any(e => e.id == id);
        }
        private bool zamestnanecExists(long id)
        {
            return _context.zamestnanec.Any(e => e.id == id);
        }
    }
}
