using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inteia.Api.Models;

namespace Inteia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecursoController : ControllerBase
    {
        private readonly AppDbContext _db;
        public RecursoController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recurso>>> Get()
            => Ok(await _db.Recursos.ToListAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Recurso>> Get(int id)
        {
            var r = await _db.Recursos.FindAsync(id);
            return r == null ? NotFound() : Ok(r);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Recurso r)
        {
            _db.Recursos.Add(r);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = r.Id }, r);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Recurso r)
        {
            if (id != r.Id) return BadRequest();
            _db.Entry(r).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var r = await _db.Recursos.FindAsync(id);
            if (r == null) return NotFound();
            _db.Recursos.Remove(r);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}