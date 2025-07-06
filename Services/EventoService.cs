using Inteia.Api.Core;
using Inteia.Api.Models;
using System.Linq.Expressions;

namespace Inteia.Api.Services;

public class EventoService : GenericService<Evento>   // 👈 Debe ser Evento
{
    public EventoService(IRepository<Evento> repo) : base(repo) { }

    protected override Expression<Func<Evento,bool>> BuildSearchExpression(string? text) =>
        string.IsNullOrWhiteSpace(text)
        ? _ => true
        : e => e.Nombre.Contains(text) || e.Ciudad.Contains(text);
}

