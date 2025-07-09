using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inteia.Api.Models;

namespace Inteia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoDeApoyoController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TipoDeApoyoController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDeApoyo>>> Get()
            => Ok(await _db.TiposDeApoyo.ToListAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoDeApoyo>> Get(int id)
        {
            var t = await _db.TiposDeApoyo.FindAsync(id);
            return t == null ? NotFound() : Ok(t);
        }

        [HttpPost]
        public async Task<ActionResult> Post(TipoDeApoyo t)
        {
            _db.TiposDeApoyo.Add(t);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = t.Id }, t);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TipoDeApoyo t)
        {
            if (id != t.Id) return BadRequest();
            _db.Entry(t).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var t = await _db.TiposDeApoyo.FindAsync(id);
            if (t == null) return NotFound();
            _db.TiposDeApoyo.Remove(t);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}