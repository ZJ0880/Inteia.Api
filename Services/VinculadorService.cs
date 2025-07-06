// Services/VinculadorService.cs
using Inteia.Api.Core;
using Inteia.Api.Models;
using System.Linq.Expressions;

namespace Inteia.Api.Services;

public class VinculadorService : GenericService<Vinculador>
{
    public VinculadorService(IRepository<Vinculador> repo) : base(repo) { }

    protected override Expression<Func<Vinculador,bool>> BuildSearchExpression(string? text) =>
        string.IsNullOrWhiteSpace(text)
        ? _ => true
        : v => v.Nombre.Contains(text) || v.Ciudad.Contains(text) || v.Correo.Contains(text);
}


