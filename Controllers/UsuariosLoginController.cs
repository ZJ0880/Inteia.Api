using Inteia.Api.Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class UsuariosLoginController : ControllerBase
{
    private readonly UsuarioLoginService _usuarioLoginService;

    public UsuariosLoginController(UsuarioLoginService usuarioLoginService)
    {
        _usuarioLoginService = usuarioLoginService;
    }

    [HttpGet]
    public ActionResult<List<UsuarioLogin>> Get() =>
        _usuarioLoginService.Get();

    [HttpGet("{id:length(24)}", Name = "GetUsuarioLogin")]
    public ActionResult<UsuarioLogin> Get(string id)
    {
        var usuario = _usuarioLoginService.Get(id);
        if (usuario == null)
            return NotFound();

        return usuario;
    }

    [HttpPost]
    public ActionResult<UsuarioLogin> Create(UsuarioLogin usuarioLogin)
    {
        _usuarioLoginService.Create(usuarioLogin);
        return CreatedAtRoute("GetUsuarioLogin", new { id = usuarioLogin.Id }, usuarioLogin);
    }

    [HttpPut("{id:length(24)}")]
    public IActionResult Update(string id, UsuarioLogin usuarioIn)
    {
        var usuario = _usuarioLoginService.Get(id);
        if (usuario == null)
            return NotFound();

        _usuarioLoginService.Update(id, usuarioIn);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public IActionResult Deactivate(string id)
    {
        var usuario = _usuarioLoginService.Get(id);
        if (usuario == null)
            return NotFound();

        _usuarioLoginService.Deactivate(id);
        return NoContent();
    }
}
