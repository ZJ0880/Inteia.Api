using Inteia.Api.Core;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ComunidadesTecNivel2Controller : GenericController<ComunidadTecNivel2>
{
    public ComunidadesTecNivel2Controller(IGenericService<ComunidadTecNivel2> service) : base(service) { }
}
