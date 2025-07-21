using Inteia.Api.Models;
using MongoDB.Driver;
using Inteia.Api.Dtos;
using Microsoft.Extensions.Options;
using Inteia.Api.Configurations;
using MongoDB.Bson;
using Inteia.Api.Services;

namespace Inteia.Api.Services
{
    public class EntidadRelacionadoraService
    {
        private readonly IMongoCollection<EntidadRelacionadora> _entidades;

        public EntidadRelacionadoraService(IOptions<MongoDBSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _entidades = database.GetCollection<EntidadRelacionadora>("EntidadRelacionadora");
        }

        public async Task<List<EntidadRelacionadoraDto>> ObtenerTodos(FiltroEntidadRelacionadoraDto filtro)
        {
            var filterBuilder = Builders<EntidadRelacionadora>.Filter;
            var filters = new List<FilterDefinition<EntidadRelacionadora>>();

            if (filtro.Tipo.HasValue)
                filters.Add(filterBuilder.Eq(e => e.Tipo, filtro.Tipo.Value));

            if (!string.IsNullOrWhiteSpace(filtro.Ciudad))
                filters.Add(filterBuilder.Regex(e => e.Ciudad, new BsonRegularExpression(filtro.Ciudad, "i")));

            if (!string.IsNullOrWhiteSpace(filtro.NombreParcial))
                filters.Add(filterBuilder.Regex(e => e.Nombre, new BsonRegularExpression(filtro.NombreParcial, "i")));

            var filter = filters.Any()
                ? filterBuilder.And(filters)
                : FilterDefinition<EntidadRelacionadora>.Empty;

            var entidades = await _entidades.Find(filter).ToListAsync();

            return entidades.Select(e => new EntidadRelacionadoraDto
            {
                Id = e.Id,
                Tipo = e.Tipo,
                Nombre = e.Nombre,
                Ciudad = e.Ciudad,
                Direccion = e.Direccion,
                Telefono = e.Telefono,
                Correo = e.Correo,
                Web = e.Web
            }).ToList();
        }

        public async Task<EntidadRelacionadoraDto> Crear(CrearEntidadRelacionadoraDto dto)
        {
            var entidad = new EntidadRelacionadora
            {
                Tipo = dto.Tipo,
                Nombre = dto.Nombre,
                Ciudad = dto.Ciudad,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                Web = dto.Web
            };

            await _entidades.InsertOneAsync(entidad);

            return new EntidadRelacionadoraDto
            {
                Id = entidad.Id,
                Tipo = entidad.Tipo,
                Nombre = entidad.Nombre,
                Ciudad = entidad.Ciudad,
                Direccion = entidad.Direccion,
                Telefono = entidad.Telefono,
                Correo = entidad.Correo,
                Web = entidad.Web
            };
        }
    }
}
