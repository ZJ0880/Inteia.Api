using Inteia.Api.Core;
using Inteia.Api.Models;
using System.Linq.Expressions;

    public class ConvocatoriaService : GenericService<Convocatoria>
    {
        public ConvocatoriaService(IRepository<Convocatoria> repo) : base(repo) { }

        protected override Expression<Func<Convocatoria, bool>> BuildSearchExpression(string? text) =>
            string.IsNullOrWhiteSpace(text)
                ? _ => true
                : c => c.Nombre.Contains(text) || c.Año.ToString().Contains(text);
    }