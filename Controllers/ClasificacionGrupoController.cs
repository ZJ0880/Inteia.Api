using Inteia.Api.Core;
using Inteia.Api.Models;
using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ClasificacionGrupoController : GenericController<ClasificacionGrupo>
    {
        public ClasificacionGrupoController(IGenericService<ClasificacionGrupo> service) : base(service) { }
    }
