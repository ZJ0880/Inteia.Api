using Inteia.Api.Models;
using Inteia.Api.DTOs;
using Inteia.Api.Interfaces;
using MongoDB.Driver;

public class ActoresCTIService : IActoresCTIService
{
    private readonly IMongoCollection<ActoresCTI> _actoresCTICollection;
    private readonly IMongoCollection<Ubicacion> _ubicacionCollection;
    private readonly IMongoCollection<GrupoInvestigacion> _grupoCollection;

    public ActoresCTIService(IMongoDatabase database)
    {
        _actoresCTICollection = database.GetCollection<ActoresCTI>("ActoresCTI");
        _ubicacionCollection = database.GetCollection<Ubicacion>("Ubicacion");
        _grupoCollection = database.GetCollection<GrupoInvestigacion>("GrupoInvestigacion");
    }

    public async Task<ActoresCTIReadDto?> CreateAsync(ActoresCTICreateDto dto)
    {
        if (!string.IsNullOrEmpty(dto.UbicacionId))
        {
            var ubicacionExists = await _ubicacionCollection.Find(u => u.Id == dto.UbicacionId).AnyAsync();
            if (!ubicacionExists) return null;
        }
        if (!string.IsNullOrEmpty(dto.GrupoInvestigacionId))
        {
            var grupoExists = await _grupoCollection.Find(g => g.Id == dto.GrupoInvestigacionId).AnyAsync();
            if (!grupoExists) return null;
        }
        var entity = new ActoresCTI
        {
            NombreEntidad = dto.NombreEntidad,
            NombreActor = dto.NombreActor,
            ReconocidoComo = dto.ReconocidoComo,
            CiudadDepartamento = dto.CiudadDepartamento,
            PaginaWeb = dto.PaginaWeb,
            Sector = dto.Sector,
            Resolucion = dto.Resolucion,
            FechaExpedicion = dto.FechaExpedicion,
            FechaNotificacion = dto.FechaNotificacion,
            VigenciaHasta = dto.VigenciaHasta,
            UbicacionId = dto.UbicacionId,
            GrupoInvestigacionId = dto.GrupoInvestigacionId
        };
        await _actoresCTICollection.InsertOneAsync(entity);
        return new ActoresCTIReadDto
        {
            Id = entity.Id,
            NombreEntidad = entity.NombreEntidad,
            NombreActor = entity.NombreActor,
            ReconocidoComo = entity.ReconocidoComo,
            CiudadDepartamento = entity.CiudadDepartamento,
            PaginaWeb = entity.PaginaWeb,
            Sector = entity.Sector,
            Resolucion = entity.Resolucion,
            FechaExpedicion = entity.FechaExpedicion,
            FechaNotificacion = entity.FechaNotificacion,
            VigenciaHasta = entity.VigenciaHasta,
            UbicacionId = entity.UbicacionId,
            GrupoInvestigacionId = entity.GrupoInvestigacionId
        };
    }

    public async Task<List<ActoresCTIReadDto>> GetAllAsync()
    {
        var entities = await _actoresCTICollection.Find(_ => true).ToListAsync();
        return entities.Select(entity => new ActoresCTIReadDto
        {
            Id = entity.Id,
            NombreEntidad = entity.NombreEntidad,
            NombreActor = entity.NombreActor,
            ReconocidoComo = entity.ReconocidoComo,
            CiudadDepartamento = entity.CiudadDepartamento,
            PaginaWeb = entity.PaginaWeb,
            Sector = entity.Sector,
            Resolucion = entity.Resolucion,
            FechaExpedicion = entity.FechaExpedicion,
            FechaNotificacion = entity.FechaNotificacion,
            VigenciaHasta = entity.VigenciaHasta,
            UbicacionId = entity.UbicacionId,
            GrupoInvestigacionId = entity.GrupoInvestigacionId
        }).ToList();
    }

    public async Task<ActoresCTIReadDto?> GetByIdAsync(string id)
    {
        var entity = await _actoresCTICollection.Find(a => a.Id == id).FirstOrDefaultAsync();
        if (entity == null) return null;
        return new ActoresCTIReadDto
        {
            Id = entity.Id,
            NombreEntidad = entity.NombreEntidad,
            NombreActor = entity.NombreActor,
            ReconocidoComo = entity.ReconocidoComo,
            CiudadDepartamento = entity.CiudadDepartamento,
            PaginaWeb = entity.PaginaWeb,
            Sector = entity.Sector,
            Resolucion = entity.Resolucion,
            FechaExpedicion = entity.FechaExpedicion,
            FechaNotificacion = entity.FechaNotificacion,
            VigenciaHasta = entity.VigenciaHasta,
            UbicacionId = entity.UbicacionId,
            GrupoInvestigacionId = entity.GrupoInvestigacionId
        };
    }

    public async Task<bool> UpdateAsync(string id, ActoresCTICreateDto dto)
    {
        var update = Builders<ActoresCTI>.Update
            .Set(x => x.NombreEntidad, dto.NombreEntidad)
            .Set(x => x.NombreActor, dto.NombreActor)
            .Set(x => x.ReconocidoComo, dto.ReconocidoComo)
            .Set(x => x.CiudadDepartamento, dto.CiudadDepartamento)
            .Set(x => x.PaginaWeb, dto.PaginaWeb)
            .Set(x => x.Sector, dto.Sector)
            .Set(x => x.Resolucion, dto.Resolucion)
            .Set(x => x.FechaExpedicion, dto.FechaExpedicion)
            .Set(x => x.FechaNotificacion, dto.FechaNotificacion)
            .Set(x => x.VigenciaHasta, dto.VigenciaHasta)
            .Set(x => x.UbicacionId, dto.UbicacionId)
            .Set(x => x.GrupoInvestigacionId, dto.GrupoInvestigacionId);
        var result = await _actoresCTICollection.UpdateOneAsync(x => x.Id == id, update);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _actoresCTICollection.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }
}
