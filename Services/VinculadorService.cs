// Services/VinculadorService.cs
using Inteia.Api.Core;
using Inteia.Api.Models;
using System.Linq.Expressions;
using Inteia.Api.Services.Interfaces;
using MongoDB.Driver;


namespace Inteia.Api.Services;

public class VinculadorService
{
    private readonly IMongoCollection<Vinculador> _vinculadores;

    public VinculadorService(IMongoDatabase database)
    {
        _vinculadores = database.GetCollection<Vinculador>("vinculadores");
    }

    public async Task<List<Vinculador>> ObtenerPorEntidadRelacionadoraId(string entidadId)
    {
        return await _vinculadores.Find(v => v.EntidadRelacionadoraId == entidadId).ToListAsync();
    }

    public async Task<Vinculador> CrearAsync(Vinculador vinculador)
    {
        await _vinculadores.InsertOneAsync(vinculador);
        return vinculador;
    }
}

