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
    public class projektsController : ControllerBase
    {
        private readonly TokenContext _context;

        public projektsController(TokenContext context)
        {
            _context = context;
        }

        // GET: api/projekts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<projekt>>> Getprojekt()
        {
            return await _context.projekt.ToListAsync();
        }

        // GET: api/projekts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<projekt>> Getprojekt(long id)
        {
            var projekt = await _context.projekt.FindAsync(id);

            if (projekt == null)
            {
                return NotFound();
            }

            return projekt;
        }

        // PUT: api/projekts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<projekt>> Putprojekt(long id, projekt projekt)
        {
            if (id != projekt.id)
            {
                return BadRequest();
            }

            var proj = _context.projekt.FirstOrDefault(s => s.id.Equals(id));
            if (proj == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(projekt.nazov))
            {
                projekt.nazov = proj.nazov;
            }
            if (String.IsNullOrEmpty(projekt.kod))
            {
                projekt.kod = proj.kod;
            }
            if (projekt.veduciID == 0)
            {
                projekt.veduciID = proj.veduciID;
            }
            if (projekt.diviziaID == 0)
            {
                projekt.diviziaID = proj.diviziaID;
            }
            if (!zamestnanecExists(projekt.veduciID))
            {
                ModelState.AddModelError(nameof(projekt.veduciID), "Employee does not exist.");
                return ValidationProblem(ModelState);
            }
            if (!diviziaExists(projekt.diviziaID))
            {
                ModelState.AddModelError(nameof(projekt.diviziaID), "Divizia does not exist.");
                return ValidationProblem(ModelState);
            }

            _context.Remove(proj);
            _context.Update(projekt);
            _context.SaveChanges();
            return projekt;
        }

        // POST: api/projekts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<projekt>> Postprojekt(projekt projekt)
        {
            if (String.IsNullOrEmpty(projekt.nazov))
            {
                ModelState.AddModelError(nameof(projekt.nazov), "Nazov is empty.");
                return ValidationProblem(ModelState);
            }
            if (String.IsNullOrEmpty(projekt.kod))
            {
                ModelState.AddModelError(nameof(projekt.kod), "Kod is empty.");
                return ValidationProblem(ModelState);
            }

            if (projektExists(projekt.id) || projekt.id < 1)
            {
                ModelState.AddModelError(nameof(projekt.id), "Id already exists or is negative.");
                return ValidationProblem(ModelState);
            }

            if (!zamestnanecExists(projekt.veduciID))
            {
                ModelState.AddModelError(nameof(projekt.veduciID), "Employee does not exist.");
                return ValidationProblem(ModelState);
            }
            if (!diviziaExists(projekt.diviziaID))
            {
                ModelState.AddModelError(nameof(projekt.diviziaID), "Divizia does not exist.");
                return ValidationProblem(ModelState);
            }
            try
            {

                _context.projekt.Add(projekt);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }

            return CreatedAtAction("Getprojekt", new { id = projekt.id }, projekt);
        }

        // DELETE: api/projekts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteprojekt(long id)
        {
            var projekt = await _context.projekt.FindAsync(id);
            if (projekt == null)
            {
                return NotFound();
            }

            try
            {
                _context.projekt.Remove(projekt);
                await _context.SaveChangesAsync();
            }
            catch 
            {
                ModelState.AddModelError(nameof(projekt.diviziaID), "Divizia has departments.");
                return ValidationProblem(ModelState);
            }

            return NoContent();
        }

        private bool projektExists(long id)
        {
            return _context.projekt.Any(e => e.id == id);
        }
        private bool zamestnanecExists(long id)
        {
            return _context.zamestnanec.Any(e => e.id == id);
        }
        private bool diviziaExists(long id)
        {
            return _context.divizia.Any(e => e.id == id);
        }
    }
}
