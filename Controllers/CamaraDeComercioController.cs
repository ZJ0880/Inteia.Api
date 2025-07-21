using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CamaraDeComercioController : ControllerBase
{
    private readonly CamaraDeComercioService _service;

    public CamaraDeComercioController(CamaraDeComercioService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CamaraDeComercioCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Created(string.Empty, dto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{vinculadorId}")]
    public async Task<IActionResult> GetById(string vinculadorId)
    {
        var result = await _service.GetByIdAsync(vinculadorId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPut("{vinculadorId}")]
    public async Task<IActionResult> Put(string vinculadorId, [FromBody] CamaraDeComercioCreateDto dto)
    {
        var updated = await _service.UpdateAsync(vinculadorId, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{vinculadorId}")]
    public async Task<IActionResult> Delete(string vinculadorId)
    {
        var deleted = await _service.DeleteAsync(vinculadorId);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
