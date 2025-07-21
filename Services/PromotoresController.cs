using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PromotoresController : ControllerBase
{
    private readonly PromotoresService _service;

    public PromotoresController(PromotoresService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult> Get() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(string id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PromotoresCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Created(string.Empty, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] PromotoresCreateDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
