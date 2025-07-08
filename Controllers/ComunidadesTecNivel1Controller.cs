using Inteia.Api.Core;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ComunidadesTecNivel1Controller : ControllerBase
{
    private readonly IGenericService<ComunidadTecNivel1> _service;

    public ComunidadesTecNivel1Controller(IGenericService<ComunidadTecNivel1> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComunidadTecNivel1>>> Get() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<ComunidadTecNivel1>> Get(string id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ComunidadTecNivel1CreateDto dto)
    {
        var entity = new ComunidadTecNivel1
        {
            Nombre = dto.Nombre,
            GratuitaOPagada = dto.GratuitaOPagada,
            Actividad = dto.Actividad,
            Lugar = dto.Lugar,
            Contactos = dto.Contactos
        };
        await _service.AddAsync(entity);
        return Created(string.Empty, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] ComunidadTecNivel1CreateDto dto)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity == null) return NotFound();

        entity.Nombre = dto.Nombre;
        entity.GratuitaOPagada = dto.GratuitaOPagada;
        entity.Actividad = dto.Actividad;
        entity.Lugar = dto.Lugar;
        entity.Contactos = dto.Contactos;

        await _service.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}