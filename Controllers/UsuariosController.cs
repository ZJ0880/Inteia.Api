using Inteia.Api.Core;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuariosController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

  
    [HttpGet]
    public ActionResult<List<Usuario>> Get() =>
        _usuarioService.Get();

   
    [HttpGet("{id:length(24)}", Name = "GetUsuario")]
    public ActionResult<Usuario> Get(string id)
    {
        var usuario = _usuarioService.Get(id);
        if (usuario == null)
            return NotFound();

        return usuario;
    }

   
    [HttpPut("{id:length(24)}")]
    public IActionResult Update(string id, Usuario usuarioIn)
    {
        var usuario = _usuarioService.Get(id);
        if (usuario == null)
            return NotFound();

        _usuarioService.Update(id, usuarioIn);
        return NoContent();
    }

 
    [HttpDelete("{id:length(24)}")]
    public IActionResult Delete(string id)
    {
        var usuario = _usuarioService.Get(id);
        if (usuario == null)
            return NotFound();

        _usuarioService.Deactivate(id); 
        return NoContent();
    }
}
