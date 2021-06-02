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
    public class oddeleniesController : ControllerBase
    {
        private readonly TokenContext _context;

        public oddeleniesController(TokenContext context)
        {
            _context = context;
        }

        // GET: api/oddelenies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<oddelenie>>> Getoddelenie()
        {
            return await _context.oddelenie.ToListAsync();
        }

        // GET: api/oddelenies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<oddelenie>> Getoddelenie(long id)
        {
            var oddelenie = await _context.oddelenie.FindAsync(id);

            if (oddelenie == null)
            {
                return NotFound();
            }

            return oddelenie;
        }

        // PUT: api/oddelenies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<oddelenie>> Putoddelenie(long id, oddelenie oddelenie)
        {
            if (id != oddelenie.id)
            {
                return BadRequest();
            }

            var odde = _context.oddelenie.FirstOrDefault(s => s.id.Equals(id));
            if (odde == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(oddelenie.nazov))
            {
                oddelenie.nazov = odde.nazov;
            }
            if (String.IsNullOrEmpty(oddelenie.kod))
            {
                oddelenie.kod = odde.kod;
            }
            if (oddelenie.veduciID == 0)
            {
                oddelenie.veduciID = odde.veduciID;
            }
            if (oddelenie.projektID == 0)
            {
                oddelenie.projektID = odde.projektID;
            }
            if (!zamestnanecExists(oddelenie.veduciID))
            {
                ModelState.AddModelError(nameof(oddelenie.veduciID), "Employee does not exist.");
                return ValidationProblem(ModelState);
            }
            if (!projektExists(oddelenie.projektID))
            {
                ModelState.AddModelError(nameof(oddelenie.projektID), "Firma does not exist.");
                return ValidationProblem(ModelState);
            }

            _context.Remove(odde);
            _context.Update(oddelenie);
            _context.SaveChanges();
            return oddelenie;
        }

        // POST: api/oddelenies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<oddelenie>> Postoddelenie(oddelenie oddelenie)
        {
            if (String.IsNullOrEmpty(oddelenie.nazov))
            {
                ModelState.AddModelError(nameof(oddelenie.nazov), "Nazov is empty.");
                return ValidationProblem(ModelState);
            }
            if (String.IsNullOrEmpty(oddelenie.kod))
            {
                ModelState.AddModelError(nameof(oddelenie.kod), "Kod is empty.");
                return ValidationProblem(ModelState);
            }

            if (oddelenieExists(oddelenie.id) || oddelenie.id < 1)
            {
                ModelState.AddModelError(nameof(oddelenie.id), "Id already exists or is negative.");
                return ValidationProblem(ModelState);
            }

            if (!zamestnanecExists(oddelenie.veduciID))
            {
                ModelState.AddModelError(nameof(oddelenie.veduciID), "Employee does not exist.");
                return ValidationProblem(ModelState);
            }
            if (!projektExists(oddelenie.projektID))
            {
                ModelState.AddModelError(nameof(oddelenie.projektID), "Projekt does not exist.");
                return ValidationProblem(ModelState);
            }
            try
            {
                _context.oddelenie.Add(oddelenie);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }

            return CreatedAtAction("Getoddelenie", new { id = oddelenie.id }, oddelenie);
        }

        // DELETE: api/oddelenies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteoddelenie(long id)
        {
            var oddelenie = await _context.oddelenie.FindAsync(id);
            if (oddelenie == null)
            {
                return NotFound();
            }

            _context.oddelenie.Remove(oddelenie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool oddelenieExists(long id)
        {
            return _context.oddelenie.Any(e => e.id == id);
        }
        private bool zamestnanecExists(long id)
        {
            return _context.zamestnanec.Any(e => e.id == id);
        }
        private bool projektExists(long id)
        {
            return _context.projekt.Any(e => e.id == id);
        }
    }
}
