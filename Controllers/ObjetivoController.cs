using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inteia.Api.Models;

namespace Inteia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObjetivoController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ObjetivoController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Objetivo>>> Get()
            => Ok(await _db.Objetivos.Include(o => o.ObjPorcentaje).ToListAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Objetivo>> Get(int id)
        {
            var o = await _db.Objetivos.Include(o => o.ObjPorcentaje).FirstOrDefaultAsync(x => x.Id == id);
            return o == null ? NotFound() : Ok(o);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Objetivo o)
        {
            _db.Objetivos.Add(o);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = o.Id }, o);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Objetivo o)
        {
            if (id != o.Id) return BadRequest();
            _db.Entry(o).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var o = await _db.Objetivos.FindAsync(id);
            if (o == null) return NotFound();
            _db.Objetivos.Remove(o);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}