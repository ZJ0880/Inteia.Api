using Inteia.Api.Core;
using Inteia.Api.Models;

namespace Inteia.Api.Controllers
{
    public class UbicacionController : GenericController<Ubicacion>
    {
        public UbicacionController(IGenericService<Ubicacion> service) : base(service) { }
    }
}
