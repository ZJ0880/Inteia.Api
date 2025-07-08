using Inteia.Api.Core;
using Inteia.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inteia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OportunidadesController : GenericController<Oportunidad>
    {
        public OportunidadesController(IGenericService<Oportunidad> service) : base(service) { }
    }
}
