using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inteia.Api.Models;

namespace Inteia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObjPorcentajeController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ObjPorcentajeController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ObjPorcentaje>>> Get()
            => Ok(await _db.ObjPorcentajes.ToListAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<ObjPorcentaje>> Get(int id)
        {
            var o = await _db.ObjPorcentajes.FindAsync(id);
            return o == null ? NotFound() : Ok(o);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ObjPorcentaje o)
        {
            if (o.Porcentaje < 0 || o.Porcentaje > 100)
                return BadRequest("El porcentaje debe ser entre 0 y 100.");

            _db.ObjPorcentajes.Add(o);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = o.Id }, o);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ObjPorcentaje o)
        {
            if (id != o.Id) return BadRequest();
            if (o.Porcentaje < 0 || o.Porcentaje > 100)
                return BadRequest("El porcentaje debe ser entre 0 y 100.");

            _db.Entry(o).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var o = await _db.ObjPorcentajes.FindAsync(id);
            if (o == null) return NotFound();
            _db.ObjPorcentajes.Remove(o);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}