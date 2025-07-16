using Inteia.Api.Core;
using Inteia.Api.Models;
using System.Linq.Expressions;

namespace Inteia.Api.Services
{
    public class GrupoInvestigacionService : GenericService<GrupoInvestigacion>
    {
        public GrupoInvestigacionService(IRepository<GrupoInvestigacion> repo) : base(repo) { }

        protected override Expression<Func<GrupoInvestigacion, bool>> BuildSearchExpression(string? text) =>
            string.IsNullOrWhiteSpace(text)
            ? _ => true
            : g => g.Nombre.Contains(text) || g.Codigo.Contains(text) || g.Lider.Contains(text);
    }
}
