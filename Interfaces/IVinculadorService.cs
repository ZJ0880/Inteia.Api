// Interfaces/IVinculadorService.cs
using Inteia.Api.Models;

namespace Inteia.Api.Services.Interfaces;

public interface IVinculadorService
{
    Task<List<Vinculador>> ObtenerTodosAsync();
    Task<Vinculador> CrearAsync(Vinculador vinculador);
    Task<List<Vinculador>> ObtenerPorEntidadRelacionadoraId(string entidadId);
}

