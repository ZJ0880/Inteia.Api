using Inteia.Api.Core;
using Inteia.Api.Models;
using Microsoft.AspNetCore.Mvc;


    [Route("api/[controller]")]
    [ApiController]
    public class ConvocatoriaController : GenericController<Convocatoria>
    {
        public ConvocatoriaController(IGenericService<Convocatoria> service) : base(service) { }
    }
