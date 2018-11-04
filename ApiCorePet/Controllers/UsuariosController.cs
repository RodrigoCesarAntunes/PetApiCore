using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiCorePet.Model;
using ApiCorePet.Criptografar;

namespace ApiCorePet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly veterinarioserviceContext _context;
        private ValidarUsuario ValidarUsuario;

        public UsuariosController(veterinarioserviceContext context)
        {
            _context = context;
            ValidarUsuario = new ValidarUsuario(context);
        }

        // GET: api/Usuarios
        [HttpGet]
        public IActionResult GetUsuario(string email, string Senha)
        {
            try
            {
                if (ValidarUsuario.TestarSenha(Senha, email) == false)
                {
                    return new UnauthorizedResult();
                }
            }
            catch (System.NullReferenceException e)
            {
                return new NotFoundResult();
            }
            Autenticacao aut = _context.Autenticacao.Find(email);
            List<Pets> pets = _context.Pets.Where(p => p.ClientePessoaEmail == email).ToList();
            ClientePessoa clientePessoa = _context.ClientePessoa.Where(c => c.UsuarioEmail == email).FirstOrDefault(); ;
            var user = _context.Usuario.Find(email);
            
            return new OkObjectResult(clientePessoa);
        }

        //// GET: api/Usuarios/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetUsuario([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var usuario = await _context.Usuario.FindAsync(id);

        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(usuario);
        //}

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario([FromRoute] string id, [FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.Email)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Senha senha = new Senha();
            string senhaCriptografada = senha.EncryptPassword(usuario.Autenticacao.Senha);
            Autenticacao aut = new Autenticacao() { Email= usuario.Email, Senha = senhaCriptografada };
           // _context.Autenticacao.Add(aut);
            usuario.Autenticacao = aut;
            //_context.Usuario.Add(usuario);

            ClientePessoa clientePessoa = new ClientePessoa() { UsuarioEmail= usuario.Email, Usuario = usuario};
            _context.ClientePessoa.Add(clientePessoa);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsuarioExists(usuario.Email))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsuario", new { id = usuario.Email }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok(usuario);
        }

        private bool UsuarioExists(string id)
        {
            return _context.Usuario.Any(e => e.Email == id);
        }
    }
}