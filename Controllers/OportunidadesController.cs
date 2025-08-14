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

        [HttpPost("scraping-simulado")]
        public async Task<IActionResult> EjecutarScrapingSimulado([FromBody] ScrapingRequest request)
        {
            try
            {
                var resultado = await _oportunidadService.WebScrapingAsyncSimulado(
                    request.Enlaces,
                    request.Cantidad,
                    request.FechaInicio,
                    request.FechaLimite
                );

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
    public class ScrapingRequest
    {
        public List<string> Enlaces { get; set; } = new();
        public int Cantidad { get; set; }
        public string FechaInicio { get; set; } = string.Empty;
        public string FechaLimite { get; set; } = string.Empty;
    }
}
