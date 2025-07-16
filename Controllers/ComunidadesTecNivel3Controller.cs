using Inteia.Api.Core;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ComunidadesTecNivel3Controller : ControllerBase
{
    private readonly IGenericService<ComunidadTecNivel3> _service;

    public ComunidadesTecNivel3Controller(IGenericService<ComunidadTecNivel3> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComunidadTecNivel3>>> Get() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<ComunidadTecNivel3>> Get(string id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ComunidadTecNivel3CreateDto dto)
    {
        var entity = new ComunidadTecNivel3
        {
            Sector = dto.Sector,
            Ubicacion1 = dto.Ubicacion1,
            Ubicacion2 = dto.Ubicacion2,
            TweetPitch = dto.TweetPitch,
            LinkedIn = dto.LinkedIn,
            Web = dto.Web,
            Telefono = dto.Telefono,
            InteresObservaciones = dto.InteresObservaciones,
            AreaRelacionamiento = dto.AreaRelacionamiento,
            EstrategiaNetworking = dto.EstrategiaNetworking,
            Contacto1 = dto.Contacto1,
            Contacto2 = dto.Contacto2,
            Ctte = dto.Ctte,
            StartupId = dto.StartupId
        };
        await _service.AddAsync(entity);
        return Created(string.Empty, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] ComunidadTecNivel3CreateDto dto)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity == null) return NotFound();

        entity.Sector = dto.Sector;
        entity.Ubicacion1 = dto.Ubicacion1;
        entity.Ubicacion2 = dto.Ubicacion2;
        entity.TweetPitch = dto.TweetPitch;
        entity.LinkedIn = dto.LinkedIn;
        entity.Web = dto.Web;
        entity.Telefono = dto.Telefono;
        entity.InteresObservaciones = dto.InteresObservaciones;
        entity.AreaRelacionamiento = dto.AreaRelacionamiento;
        entity.EstrategiaNetworking = dto.EstrategiaNetworking;
        entity.Contacto1 = dto.Contacto1;
        entity.Contacto2 = dto.Contacto2;
        entity.Ctte = dto.Ctte;
        entity.StartupId = dto.StartupId;

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