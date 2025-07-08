using Inteia.Api.Core;
using Inteia.Api.Models;
using System.Linq.Expressions;

    public class ProgramasColcienciasService : GenericService<ProgramasColciencias>
    {
        public ProgramasColcienciasService(IRepository<ProgramasColciencias> repo) : base(repo) { }

        protected override Expression<Func<ProgramasColciencias, bool>> BuildSearchExpression(string? text) =>
            string.IsNullOrWhiteSpace(text)
                ? _ => true
                : p => p.NombreProgramaColciencias.Contains(text) || p.Codigo.Contains(text);
    }
