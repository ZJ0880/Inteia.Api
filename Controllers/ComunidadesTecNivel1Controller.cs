using Inteia.Api.Core;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ComunidadesTecNivel1Controller : GenericController<ComunidadTecNivel1>
{
    public ComunidadesTecNivel1Controller(IGenericService<ComunidadTecNivel1> service) : base(service) { }
}
