using Inteia.Api.Core;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ComunidadesTecNivel3Controller : GenericController<ComunidadTecNivel3>
{
    public ComunidadesTecNivel3Controller(IGenericService<ComunidadTecNivel3> service) : base(service) { }
}
