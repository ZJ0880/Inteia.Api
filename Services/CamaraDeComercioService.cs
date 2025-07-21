using Inteia.Api.Models;
using MongoDB.Driver;

public class CamaraDeComercioService
{
    private readonly IMongoCollection<CamaraDeComercio> _collection;
    private readonly IMongoCollection<Vinculador> _vinculadorCollection;

    public CamaraDeComercioService(IMongoDatabase db)
    {
        _collection = db.GetCollection<CamaraDeComercio>("CamaraDeComercio");
        _vinculadorCollection = db.GetCollection<Vinculador>("Vinculador");
    }

    public async Task CreateAsync(CamaraDeComercioCreateDto dto)
    {
        var vinculador = new Vinculador
        {
            Tipo = TipoVinculador.CamaraDeComercio,
            Nombre = dto.Nombre,
            Ciudad = dto.Ciudad,
            Direccion = dto.Direccion,
            Telefono = dto.Telefono,
            Correo = dto.Correo,
            Web = dto.Web
        };

        await _vinculadorCollection.InsertOneAsync(vinculador);

        var camara = new CamaraDeComercio
        {
            VinculadorId = vinculador.Id,
            Cargo = dto.Cargo,
            Municipio = dto.Municipio,
            Lider = dto.Lider
        };

        await _collection.InsertOneAsync(camara);
    }

    public async Task<List<CamaraDeComercio>> GetAllAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<CamaraDeComercio?> GetByIdAsync(string vinculadorId) =>
        await _collection.Find(x => x.VinculadorId == vinculadorId).FirstOrDefaultAsync();

    public async Task<bool> UpdateAsync(string vinculadorId, CamaraDeComercioCreateDto dto)
    {
        var vinculadorUpdate = Builders<Vinculador>.Update
            .Set(x => x.Nombre, dto.Nombre)
            .Set(x => x.Ciudad, dto.Ciudad)
            .Set(x => x.Direccion, dto.Direccion)
            .Set(x => x.Telefono, dto.Telefono)
            .Set(x => x.Correo, dto.Correo)
            .Set(x => x.Web, dto.Web);

        var vinculadorResult = await _vinculadorCollection.UpdateOneAsync(x => x.Id == vinculadorId, vinculadorUpdate);

        var camaraUpdate = Builders<CamaraDeComercio>.Update
            .Set(x => x.Cargo, dto.Cargo)
            .Set(x => x.Municipio, dto.Municipio)
            .Set(x => x.Lider, dto.Lider);

        var camaraResult = await _collection.UpdateOneAsync(x => x.VinculadorId == vinculadorId, camaraUpdate);

        return vinculadorResult.ModifiedCount > 0 && camaraResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string vinculadorId)
    {
        var camaraResult = await _collection.DeleteOneAsync(x => x.VinculadorId == vinculadorId);
        var vinculadorResult = await _vinculadorCollection.DeleteOneAsync(x => x.Id == vinculadorId);
        return camaraResult.DeletedCount > 0 && vinculadorResult.DeletedCount > 0;
    }
}
