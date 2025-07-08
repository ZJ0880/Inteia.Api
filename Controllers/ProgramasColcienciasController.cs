using Inteia.Api.Core;
using Inteia.Api.Models;
using Microsoft.AspNetCore.Mvc;


    [Route("api/[controller]")]
    [ApiController]
    public class ProgramasColcienciasController : GenericController<ProgramasColciencias>
    {
        public ProgramasColcienciasController(IGenericService<ProgramasColciencias> service) : base(service) { }
    }

