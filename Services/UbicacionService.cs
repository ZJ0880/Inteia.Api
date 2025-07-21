using Inteia.Api.Models;
using Inteia.Api.DTOs;
using Inteia.Api.Interfaces;
using MongoDB.Driver;

public class UbicacionService : IUbicacionService
{
    private readonly IMongoCollection<Ubicacion> _ubicacionCollection;

    public UbicacionService(IMongoDatabase database)
    {
        _ubicacionCollection = database.GetCollection<Ubicacion>("Ubicacion");
    }

    public async Task<UbicacionReadDto?> CreateAsync(UbicacionCreateDto dto)
    {
        var entity = new Ubicacion
        {
            Municipio = dto.Municipio,
            Departamento = dto.Departamento,
            Region = dto.Region,
            Pais = dto.Pais,
            CodDane = dto.CodDane
        };
        await _ubicacionCollection.InsertOneAsync(entity);
        return new UbicacionReadDto
        {
            Id = entity.Id,
            Municipio = entity.Municipio,
            Departamento = entity.Departamento,
            Region = entity.Region,
            Pais = entity.Pais,
            CodDane = entity.CodDane
        };
    }

    public async Task<List<UbicacionReadDto>> GetAllAsync()
    {
        var entities = await _ubicacionCollection.Find(_ => true).ToListAsync();
        return entities.Select(entity => new UbicacionReadDto
        {
            Id = entity.Id,
            Municipio = entity.Municipio,
            Departamento = entity.Departamento,
            Region = entity.Region,
            Pais = entity.Pais,
            CodDane = entity.CodDane
        }).ToList();
    }

    public async Task<UbicacionReadDto?> GetByIdAsync(string id)
    {
        var entity = await _ubicacionCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
        if (entity == null) return null;
        return new UbicacionReadDto
        {
            Id = entity.Id,
            Municipio = entity.Municipio,
            Departamento = entity.Departamento,
            Region = entity.Region,
            Pais = entity.Pais,
            CodDane = entity.CodDane
        };
    }

    public async Task<bool> UpdateAsync(string id, UbicacionCreateDto dto)
    {
        var update = Builders<Ubicacion>.Update
            .Set(x => x.Municipio, dto.Municipio)
            .Set(x => x.Departamento, dto.Departamento)
            .Set(x => x.Region, dto.Region)
            .Set(x => x.Pais, dto.Pais)
            .Set(x => x.CodDane, dto.CodDane);
        var result = await _ubicacionCollection.UpdateOneAsync(x => x.Id == id, update);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _ubicacionCollection.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }
}
