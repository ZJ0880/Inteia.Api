using Inteia.Api.Core;
using Inteia.Api.Models;
using Inteia.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ActoresCTIController : ControllerBase
{
    private readonly IGenericService<ActoresCTI> _service;
    public ActoresCTIController(IGenericService<ActoresCTI> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActoresCTI>>> Get() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<ActoresCTI>> Get(string id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ActoresCTICreateDto dto)
    {
        var entity = new ActoresCTI
        {
            InteresInteia = dto.InteresInteia ?? string.Empty,
            NombreEntidad = dto.NombreEntidad ?? string.Empty,
            NombreActor = dto.NombreActor ?? string.Empty,
            ReconocidoComo = dto.ReconocidoComo ?? string.Empty,
            CiudadDepartamento = dto.CiudadDepartamento ?? string.Empty,
            PaginaWeb = dto.PaginaWeb ?? string.Empty,
            Sector = dto.Sector ?? string.Empty,
            Resolucion = dto.Resolucion ?? string.Empty,
            FechaExpedicion = dto.FechaExpedicion,
            FechaNotificacion = dto.FechaNotificacion,
            VigenciaHasta = dto.VigenciaHasta,
            GrupoInvestigacionId = dto.GrupoInvestigacionId
        };
        await _service.AddAsync(entity);
        // Return a 201 Created response with the location of the new resource.
        return Created(string.Empty, entity);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] ActoresCTICreateDto dto)
{

    var entity = await _service.GetByIdAsync(id);

    if (entity == null)
    {
        
        return NotFound();
    }

    entity.InteresInteia = dto.InteresInteia ?? string.Empty;
    entity.NombreEntidad = dto.NombreEntidad ?? string.Empty;
    entity.NombreActor = dto.NombreActor ?? string.Empty;
    entity.ReconocidoComo = dto.ReconocidoComo ?? string.Empty;
    entity.CiudadDepartamento = dto.CiudadDepartamento ?? string.Empty;
    entity.PaginaWeb = dto.PaginaWeb ?? string.Empty;
    entity.Sector = dto.Sector ?? string.Empty;
    entity.Resolucion = dto.Resolucion ?? string.Empty;
    entity.FechaExpedicion = dto.FechaExpedicion;
    entity.FechaNotificacion = dto.FechaNotificacion;
    entity.VigenciaHasta = dto.VigenciaHasta;
    entity.GrupoInvestigacionId = dto.GrupoInvestigacionId;

    
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

