using Microsoft.AspNetCore.Mvc;
using Inteia.Api.Models;
using Inteia.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inteia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            var usuarios = await _usuarioService.GetAsync();
            return Ok(usuarios);
        }

        // GET: api/usuarios/{id}
        [HttpGet("{id:length(24)}", Name = "GetUsuario")]
        public async Task<ActionResult<Usuario>> Get(string id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);

            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado" });
            }

            return Ok(usuario);
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> Create(Usuario nuevoUsuario)
        {
            await _usuarioService.CreateAsync(nuevoUsuario);
            return CreatedAtRoute("GetUsuario", new { id = nuevoUsuario.Id }, nuevoUsuario);
        }

        // PUT: api/usuarios/{id}
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Usuario usuarioIn)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);

            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado para actualizar" });
            }

            usuarioIn.Id = id; // aseguramos que conserve el mismo id
            await _usuarioService.UpdateAsync(id, usuarioIn);

            return NoContent(); // 204
        }

        // DELETE: api/usuarios/{id}
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);

            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado para eliminar" });
            }

            await _usuarioService.DeleteAsync(id);
            return NoContent(); // eliminación lógica (IsActive = false)
        }
    }
}
