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
    public class PetsController : ControllerBase
    {
        private readonly veterinarioserviceContext _context;
        private ValidarUsuario ValidarUsuario;

        public PetsController(veterinarioserviceContext context)
        {
            _context = context;
            ValidarUsuario = new ValidarUsuario(context);
        }

        // GET: api/Pets
        [HttpGet]
        public IEnumerable<Pets> GetPets()
        {
            return _context.Pets;
            
        }

        // GET: api/Pets/5
        [HttpGet]
        public IActionResult GetPets(string email, string senha)
        {
            if (ValidarUsuario.TestarSenha(senha, email) == false)
            {
                return new UnauthorizedResult();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pets = _context.Pets.Where(p => p.ClientePessoaEmail == email).FirstOrDefault();

            if (pets == null)
            {
                return NotFound();
            }

            return Ok(pets);
        }

        // PUT: api/Pets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPets([FromRoute] int id, [FromBody] Pets pets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pets.PetId)
            {
                return BadRequest();
            }

            _context.Entry(pets).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetsExists(id))
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

        // POST: api/Pets
        [HttpPost]
        public async Task<IActionResult> PostPets([FromBody] Pets pets)
        {
            if (ValidarUsuario.TestarSenha(pets.ClientePessoa.Usuario.Autenticacao.Senha, 
                pets.ClientePessoa.Usuario.Autenticacao.Email) == false)
            {
                return new UnauthorizedResult();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            pets.ClientePessoa = null;
            
            _context.Pets.Add(pets);
            try
            {
                _context.SaveChanges();
            }catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            

            return CreatedAtAction("GetPets", new { id = pets.PetId }, pets);
        }

        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePets([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pets = await _context.Pets.FindAsync(id);
            if (pets == null)
            {
                return NotFound();
            }

            _context.Pets.Remove(pets);
            await _context.SaveChangesAsync();

            return Ok(pets);
        }

        private bool PetsExists(int id)
        {
            return _context.Pets.Any(e => e.PetId == id);
        }
    }
}