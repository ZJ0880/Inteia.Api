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

        public async Task<object?> WebScrapingAsyncSimulado(
            List<string> enlaces,
            int cantidad,
            string fechaInicio,
            string fechaLimite)
        {
            var resultados = new List<object>();
            var random = new Random();

            foreach (var enlace in enlaces)
            {
                for (int i = 0; i < cantidad; i++)
                {
                    bool esPostulable = random.Next(0, 2) == 1;

                    resultados.Add(new
                    {
                        descripcion = $"Oportunidad  {i + 1} para {enlace}",
                        esPostulable = esPostulable,
                        motivoPostulable = esPostulable ? "Cumple requisitos " : null,
                        motivoNoPostulable = !esPostulable ? "No cumple requisitos " : null,
                        codigo = $"COD-{random.Next(1000, 9999)}",
                        fuente = enlace,
                        fechaInicio,
                        fechaLimite
                    });
                }
            }
            return resultados;
        }
    }
}
