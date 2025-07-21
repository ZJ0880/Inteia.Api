using Inteia.Api.Models;
using MongoDB.Driver;

public class AgremiacionesService
{
    private readonly IMongoCollection<Agremiaciones> _collection;
    private readonly IMongoCollection<Vinculador> _vinculadorCollection;

    public AgremiacionesService(IMongoDatabase db)
    {
        _collection = db.GetCollection<Agremiaciones>("Agremiaciones");
        _vinculadorCollection = db.GetCollection<Vinculador>("Vinculador");
    }

    public async Task CreateAsync(AgremiacionesCreateDto dto)
    {
        var vinculador = new Vinculador
        {
            Tipo = TipoVinculador.Agremiaciones,
            Nombre = dto.Nombre,
            Ciudad = dto.Ciudad,
            Direccion = dto.Direccion,
            Telefono = dto.Telefono,
            Correo = dto.Correo,
            Web = dto.Web
        };

        await _vinculadorCollection.InsertOneAsync(vinculador);

        var agremiacion = new Agremiaciones
        {
            VinculadorId = vinculador.Id,
            Sigla = dto.Sigla,
            RazonSocial = dto.RazonSocial
        };

        await _collection.InsertOneAsync(agremiacion);
    }

    public async Task<List<Agremiaciones>> GetAllAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<Agremiaciones?> GetByIdAsync(string vinculadorId) =>
        await _collection.Find(x => x.VinculadorId == vinculadorId).FirstOrDefaultAsync();

    public async Task<bool> UpdateAsync(string vinculadorId, AgremiacionesCreateDto dto)
    {
        var vinculadorUpdate = Builders<Vinculador>.Update
            .Set(x => x.Nombre, dto.Nombre)
            .Set(x => x.Ciudad, dto.Ciudad)
            .Set(x => x.Direccion, dto.Direccion)
            .Set(x => x.Telefono, dto.Telefono)
            .Set(x => x.Correo, dto.Correo)
            .Set(x => x.Web, dto.Web);

        var vinculadorResult = await _vinculadorCollection.UpdateOneAsync(x => x.Id == vinculadorId, vinculadorUpdate);

        var agremiacionUpdate = Builders<Agremiaciones>.Update
            .Set(x => x.Sigla, dto.Sigla)
            .Set(x => x.RazonSocial, dto.RazonSocial);

        var agremiacionResult = await _collection.UpdateOneAsync(x => x.VinculadorId == vinculadorId, agremiacionUpdate);

        return vinculadorResult.ModifiedCount > 0 && agremiacionResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string vinculadorId)
    {
        var agremiacionResult = await _collection.DeleteOneAsync(x => x.VinculadorId == vinculadorId);
        var vinculadorResult = await _vinculadorCollection.DeleteOneAsync(x => x.Id == vinculadorId);
        return agremiacionResult.DeletedCount > 0 && vinculadorResult.DeletedCount > 0;
    }
}
