using Inteia.Api.Core;
using Microsoft.AspNetCore.Mvc;

// Controllers/EventosController.cs
[Route("api/[controller]")]
[ApiController]
public class EventosController : GenericController<Evento>
{
    public EventosController(IGenericService<Evento> service) : base(service) { }
}
