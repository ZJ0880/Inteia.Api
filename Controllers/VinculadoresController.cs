using Inteia.Api.Core;
using Inteia.Api.Models;
using Inteia.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inteia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VinculadoresController : GenericController<Vinculador>
{
    public VinculadoresController(IGenericService<Vinculador> service)
     : base(service) { }
}
