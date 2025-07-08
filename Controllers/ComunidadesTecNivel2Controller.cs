using Inteia.Api.Core;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ComunidadesTecNivel2Controller : ControllerBase
{
    private readonly IGenericService<ComunidadTecNivel2> _service;

    public ComunidadesTecNivel2Controller(IGenericService<ComunidadTecNivel2> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComunidadTecNivel2>>> Get() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<ComunidadTecNivel2>> Get(string id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ComunidadTecNivel2CreateDto dto)
    {
        var entity = new ComunidadTecNivel2
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Link = dto.Link,
            Pais = dto.Pais,
            Industria = dto.Industria,
            ComunidadId = dto.ComunidadId
        };
        await _service.AddAsync(entity);
        return Created(string.Empty, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] ComunidadTecNivel2CreateDto dto)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity == null) return NotFound();

        entity.Nombre = dto.Nombre;
        entity.Descripcion = dto.Descripcion;
        entity.Link = dto.Link;
        entity.Pais = dto.Pais;
        entity.Industria = dto.Industria;
        entity.ComunidadId = dto.ComunidadId;

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
