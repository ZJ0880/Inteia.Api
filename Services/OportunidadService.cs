using Inteia.Api.Core;
using Inteia.Api.Models;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text.Json;

namespace Inteia.Api.Services
{
    public class OportunidadService : GenericService<Oportunidad>
    {
        public OportunidadService(IRepository<Oportunidad> repo) : base(repo) { }

        protected override Expression<Func<Oportunidad, bool>> BuildSearchExpression(string? text) =>
            string.IsNullOrWhiteSpace(text)
            ? _ => true
            : o => o.idProceso.Contains(text) || o.descripcion.Contains(text) || o.departamento.Contains(text);

        public async Task<object?> WebScrapingAsync(string parametro)
        {
            string pythonPath = "python3"; 
            string scriptPath = Path.Combine(AppContext.BaseDirectory, "Scripts", "script.py");

            var psi = new ProcessStartInfo
            {
                FileName = pythonPath,
                Arguments = $"\"{scriptPath}\" \"{parametro}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = psi };
            process.Start();

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();
            process.WaitForExit();

            if (!string.IsNullOrEmpty(error))
                throw new Exception($"Error ejecutando script Python: {error}");

            try
            {
                return JsonSerializer.Deserialize<object>(output);
            }
            catch (JsonException ex)
            {
                throw new Exception("El script Python no devolvió un JSON válido", ex);
            }
        }
    }
}
