using Inteia.Api.Core;
using Inteia.Api.Models;
using Inteia.Api.DTOs;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class UbicacionController : ControllerBase
{
    private readonly IGenericService<Ubicacion> _service;
    public UbicacionController(IGenericService<Ubicacion> service)
    {
        _service = service;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ubicacion>>> Get() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Ubicacion>> Get(string id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UbicacionCreateDto dto)
    {
        var entity = new Ubicacion
        {
            Municipio = dto.Municipio ?? string.Empty,
            Departamento = dto.Departamento ?? string.Empty,
            Region = dto.Region ?? string.Empty,
            Pais = dto.Pais ?? string.Empty,
            CodDane = dto.CodDane ?? string.Empty
        };
        await _service.AddAsync(entity);
        return Created(string.Empty, entity);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] UbicacionCreateDto dto)
    {

        var entity = await _service.GetByIdAsync(id);

        if (entity == null)
        {
            
            return NotFound();
        }

        entity.Municipio = dto.Municipio ?? string.Empty;
        entity.Departamento = dto.Departamento ?? string.Empty;
        entity.Region = dto.Region ?? string.Empty;
        entity.Pais = dto.Pais ?? string.Empty;
        entity.CodDane = dto.CodDane ?? string.Empty;

        
        await _service.UpdateAsync(entity);
        // Return a 204 No Content response, which is common for successful PUT operations that don't return data.
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}

