using Inteia.Api.Core;
using Inteia.Api.Models;

namespace Inteia.Api.Controllers
{
    public class ActoresCTIController : GenericController<ActoresCTI>
    {
        public ActoresCTIController(IGenericService<ActoresCTI> service) : base(service) { }
    }
}
