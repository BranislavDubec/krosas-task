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
    public class diviziasController : ControllerBase
    {
        private readonly TokenContext _context;

        public diviziasController(TokenContext context)
        {
            _context = context;
        }

        // GET: api/divizias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<divizia>>> Getdivizia()
        {
            return await _context.divizia.ToListAsync();
        }

        // GET: api/divizias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<divizia>> Getdivizia(long id)
        {
            var divizia = await _context.divizia.FindAsync(id);

            if (divizia == null)
            {
                return NotFound();
            }

            return divizia;
        }

        // PUT: api/divizias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<divizia>> Putdivizia(long id, divizia divizia)
        {
            if (id != divizia.id)
            {
                return BadRequest();
            }

            var div = _context.divizia.FirstOrDefault(s => s.id.Equals(id));
            if (div == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(divizia.nazov))
            {
                divizia.nazov = div.nazov;
            }
            if (String.IsNullOrEmpty(divizia.kod))
            {
                divizia.kod = div.kod;
            }
            if (divizia.veduciID == 0) {
                divizia.veduciID = div.veduciID;
            }
            if (divizia.firmaID == 0)
            {
                divizia.firmaID = div.firmaID;
            }
            if (!zamestnanecExists(divizia.veduciID))
            {
                ModelState.AddModelError(nameof(divizia.veduciID), "Employee does not exist. " +  divizia.veduciID );
                return ValidationProblem(ModelState);
            }
            if (!firmaExists(divizia.firmaID))
            {
                ModelState.AddModelError(nameof(divizia.firmaID), "Firma does not exist.");
                return ValidationProblem(ModelState);
            }

            _context.Remove(div);
            _context.Update(divizia);
            _context.SaveChanges();
            return divizia;
        }

        // POST: api/divizias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<divizia>> Postdivizia(divizia divizia)
        {
            if (String.IsNullOrEmpty(divizia.nazov))
            {
                ModelState.AddModelError(nameof(divizia.nazov), "Nazov is empty.");
                return ValidationProblem(ModelState);
            }
            if (String.IsNullOrEmpty(divizia.kod))
            {
                ModelState.AddModelError(nameof(divizia.kod), "Kod is empty.");
                return ValidationProblem(ModelState);
            }

            if (diviziaExists(divizia.id) || divizia.id < 1)
            {
                ModelState.AddModelError(nameof(divizia.id), "Id already exists or is negative.");
                return ValidationProblem(ModelState);
            }

            if (!zamestnanecExists(divizia.veduciID))
            {
                ModelState.AddModelError(nameof(divizia.veduciID), "Employee does not exist.");
                return ValidationProblem(ModelState);
            }
            if (!firmaExists(divizia.firmaID))
            {
                ModelState.AddModelError(nameof(divizia.firmaID), "Firma does not exist.");
                return ValidationProblem(ModelState);
            }
            try
            {
                _context.divizia.Add(divizia);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }

            return CreatedAtAction("Getdivizia", new { id = divizia.id }, divizia);
        }

        // DELETE: api/divizias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletedivizia(long id)
        {
            var divizia = await _context.divizia.FindAsync(id);
            if (divizia == null)
            {
                return NotFound();
            }

            try
            {
                _context.divizia.Remove(divizia);
                await _context.SaveChangesAsync();
            }
            catch
            {
                ModelState.AddModelError(nameof(divizia.id), "Divizia has projects.");
                return ValidationProblem(ModelState);
            }

            

            return NoContent();
        }

        private bool diviziaExists(long id)
        {
            return _context.divizia.Any(e => e.id == id);
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
