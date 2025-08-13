using Inteia.Api.Core;
using Inteia.Api.Models;
using Inteia.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inteia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OportunidadesController : GenericController<Oportunidad>
    {
        private readonly OportunidadService _oportunidadService;

        public OportunidadesController(IGenericService<Oportunidad> service)
            : base(service)
        {
            _oportunidadService = (OportunidadService)service;
        }


        [HttpGet("scraping")]
        public async Task<IActionResult> EjecutarScraping([FromQuery] string parametro)
        {
            try
            {
                var resultado = await _oportunidadService.WebScrapingAsync(parametro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
