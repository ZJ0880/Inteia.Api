using Inteia.Api.Core;
using Inteia.Api.Models;
using System.Linq.Expressions;

namespace Inteia.Api.Services
{
    public class OportunidadService : GenericService<Oportunidad>
    {
        public OportunidadService(IRepository<Oportunidad> repo) : base(repo) { }

        protected override Expression<Func<Oportunidad, bool>> BuildSearchExpression(string? text) =>
            string.IsNullOrWhiteSpace(text)
            ? _ => true
            : o => o.idProceso.Contains(text) || o.descripcion.Contains(text) || o.departamento.Contains(text);
    }
}
