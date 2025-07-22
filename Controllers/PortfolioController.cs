using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inteia.Api.Models;

namespace Inteia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly AppDbContext _db;
        public PortfolioController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Portfolio>>> Get()
            => Ok(await _db.Portfolios
                           .Include(p => p.Objetivos)
                           .Include(p => p.Recursos)
                           .Include(p => p.Instrumentos)
                           .ToListAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Portfolio>> Get(int id)
        {
            var p = await _db.Portfolios
                             .Include(p => p.Objetivos)
                             .Include(p => p.Recursos)
                             .Include(p => p.Instrumentos)
                             .FirstOrDefaultAsync(x => x.Id == id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Portfolio p)
        {
            if (p.FechaCierre < p.FechaApertura)
                return BadRequest("La fecha de cierre no puede ser anterior a la apertura.");

            if (await _db.Portfolios.AnyAsync(x => x.Nombre == p.Nombre))
                return Conflict("El nombre ya existe.");

            _db.Portfolios.Add(p);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = p.Id }, p);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Portfolio p)
        {
            if (id != p.Id) return BadRequest();
            _db.Entry(p).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Portfolios.FindAsync(id);
            if (p == null) return NotFound();
            _db.Portfolios.Remove(p);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}