using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inteia.Api.Models;

namespace Inteia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstrumentoController : ControllerBase
    {
        private readonly AppDbContext _db;
        public InstrumentoController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instrumento>>> Get()
            => Ok(await _db.Instrumentos.ToListAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Instrumento>> Get(int id)
        {
            var i = await _db.Instrumentos.FindAsync(id);
            return i == null ? NotFound() : Ok(i);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Instrumento i)
        {
            _db.Instrumentos.Add(i);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = i.Id }, i);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Instrumento i)
        {
            if (id != i.Id) return BadRequest();
            _db.Entry(i).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var i = await _db.Instrumentos.FindAsync(id);
            if (i == null) return NotFound();
            _db.Instrumentos.Remove(i);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}