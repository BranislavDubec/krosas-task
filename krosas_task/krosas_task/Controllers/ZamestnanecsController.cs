using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using krosas_task.Models;
using System.Net.Mail;

namespace krosas_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class zamestnanecsController : ControllerBase
    {
        private readonly TokenContext _context;

        public zamestnanecsController(TokenContext context)
        {
            _context = context;
        }

        // GET: api/zamestnanecs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<zamestnanec>>> Getzamestnanecetnanec()
        {
            return await _context.zamestnanec.ToListAsync();
        }

        // GET: api/zamestnanecs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<zamestnanec>> Getzamestnanec(long id)
        {
            var zamestnanec = await _context.zamestnanec.FindAsync(id);

            if (zamestnanec == null)
            {
                return NotFound();
            }

            return zamestnanec;
        }

        // PUT: api/zamestnanecs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<zamestnanec>> Putzamestnanec(long id, zamestnanec zamestnanec)
        {
            if (id != zamestnanec.id)
            {
                return BadRequest();
            }
            var zam = _context.zamestnanec.FirstOrDefault(s => s.id.Equals(id));

            if (zam == null)
            {
                return NotFound();
            }
            if (String.IsNullOrEmpty(zamestnanec.meno))
            {
                zamestnanec.meno = zam.meno;
            }
            if (String.IsNullOrEmpty(zamestnanec.priezvisko))
            {
                zamestnanec.priezvisko = zam.priezvisko;
            }
            if (String.IsNullOrEmpty(zamestnanec.email))
            {
                zamestnanec.email = zam.email;
            }
            if (String.IsNullOrEmpty(zamestnanec.titul))
            {
                zamestnanec.titul = zam.titul;
            }
            if (String.IsNullOrEmpty(zamestnanec.telefon))
            {
                zamestnanec.telefon = zam.telefon;
            }
            else
            {
                try
                {
                    MailAddress m = new MailAddress(zamestnanec.email);
                }
                catch (FormatException)
                {
                    ModelState.AddModelError(nameof(zamestnanec.email), "Email is wrong.");
                    return ValidationProblem(ModelState);
                }
            }

            _context.Remove(zam);
            _context.Update(zamestnanec);
            _context.SaveChanges();

            return zamestnanec;
        }

        // POST: api/zamestnanecs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<zamestnanec>> Postzamestnanec(zamestnanec zamestnanec)
        {

            
            if (String.IsNullOrEmpty(zamestnanec.meno)) {
                ModelState.AddModelError(nameof(zamestnanec.meno), "Meno is empty.");
                return ValidationProblem(ModelState);
            }
            if (String.IsNullOrEmpty(zamestnanec.priezvisko)) {
                ModelState.AddModelError(nameof(zamestnanec.priezvisko), "Priezvisko is empty.");
                return ValidationProblem(ModelState);
            }
            try
            {
                MailAddress m = new MailAddress(zamestnanec.email);
            }
            catch (FormatException)
            {
                ModelState.AddModelError(nameof(zamestnanec.email), "Email is wrong.");
                return ValidationProblem(ModelState);
            }
            if (zamestnanecExists(zamestnanec.id) || zamestnanec.id < 1){
                ModelState.AddModelError(nameof(zamestnanec.id), "Id already exists or is negative.");
                return ValidationProblem(ModelState);
            }

            try
            {

                _context.zamestnanec.Add(zamestnanec);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }
            return CreatedAtAction("Getzamestnanec", new { id = zamestnanec.id }, zamestnanec);
        }

        // DELETE: api/zamestnanecs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletezamestnanec(long id)
        {
            var zamestnanec = await _context.zamestnanec.FindAsync(id);
            if (zamestnanec == null)
            {
                return NotFound();
            }
            try
            {
                _context.zamestnanec.Remove(zamestnanec);
                await _context.SaveChangesAsync();
            }
            catch 
            {
                ModelState.AddModelError(nameof(zamestnanec.id), "Employee is a node leader.");
                return ValidationProblem(ModelState);
            }
            return NoContent();
        }

        private bool zamestnanecExists(long  id)
        {
            return _context.zamestnanec.Any(e => e.id == id);
        }
    }
}
