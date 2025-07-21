using Inteia.Api.Core;
using Inteia.Api.Models;

namespace Inteia.Api.Controllers
{
    public class GrupoInvestigacionController : GenericController<GrupoInvestigacion>
    {
        public GrupoInvestigacionController(IGenericService<GrupoInvestigacion> service) : base(service) { }
    }
}
