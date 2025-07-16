using Inteia.Api.Core;
using Inteia.Api.Models;
using System.Linq.Expressions;

    public class ClasificacionGrupoService : GenericService<ClasificacionGrupo>
    {
        public ClasificacionGrupoService(IRepository<ClasificacionGrupo> repo) : base(repo) { }

        protected override Expression<Func<ClasificacionGrupo, bool>> BuildSearchExpression(string? text) =>
            string.IsNullOrWhiteSpace(text)
                ? _ => true
                : c => c.NombreClasificacion.Contains(text) || c.Descripcion.Contains(text);
    }
