using Inteia.Api.Dtos;
using Inteia.Api.Models;
using Inteia.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inteia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EntidadRelacionadoraController : ControllerBase
{
    private readonly EntidadRelacionadoraService _service;

    public EntidadRelacionadoraController(EntidadRelacionadoraService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<EntidadRelacionadoraDto>> Crear([FromBody] CrearEntidadRelacionadoraDto dto)
    {
        var nueva = await _service.Crear(dto);
        return CreatedAtAction(nameof(Crear), new { id = nueva.Id }, nueva);
    }

    [HttpGet]
    public async Task<ActionResult<List<EntidadRelacionadoraDto>>> ObtenerTodos([FromQuery] FiltroEntidadRelacionadoraDto filtro)
    {
        var resultado = await _service.ObtenerTodos(filtro);
        return Ok(resultado);
    }
}
