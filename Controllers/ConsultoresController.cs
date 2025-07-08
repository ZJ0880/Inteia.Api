using Inteia.Api.Core;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ConsultoresController : GenericController<Consultor>
{
    public ConsultoresController(IGenericService<Consultor> service) : base(service) { }
}
