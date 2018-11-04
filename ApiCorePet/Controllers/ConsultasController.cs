using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiCorePet.Model;

namespace ApiCorePet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly veterinarioserviceContext _context;

        public ConsultasController(veterinarioserviceContext context)
        {
            _context = context;
        }

        // GET: api/Consultas
        [HttpGet]
        public IEnumerable<Consulta> GetConsulta()
        {
            return _context.Consulta;
        }

        // GET: api/Consultas/5
        [HttpGet]
        public async Task<IActionResult> GetConsulta(string email, string senha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var consulta = await _context.Consulta.FindAsync(email);

            if (consulta == null)
            {
                return NotFound();
            }

            return Ok(consulta);
        }

        // PUT: api/Consultas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsulta([FromRoute] int id, [FromBody] Consulta consulta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != consulta.ConsultaId)
            {
                return BadRequest();
            }

            _context.Entry(consulta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultaExists(id))
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

        // POST: api/Consultas
        [HttpPost]
        public async Task<IActionResult> PostConsulta([FromBody] Consulta consulta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Consulta.Add(consulta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsulta", new { id = consulta.ConsultaId }, consulta);
        }

        // DELETE: api/Consultas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsulta([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var consulta = await _context.Consulta.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }

            _context.Consulta.Remove(consulta);
            await _context.SaveChangesAsync();

            return Ok(consulta);
        }

        private bool ConsultaExists(int id)
        {
            return _context.Consulta.Any(e => e.ConsultaId == id);
        }
    }
}