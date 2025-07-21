using MongoDB.Driver;

public class HabilitadoresService
{
    private readonly IMongoCollection<Habilitadores> _collection;

    public HabilitadoresService(IMongoDatabase db)
    {
        _collection = db.GetCollection<Habilitadores>("Habilitadores");
    }

    public async Task CreateAsync(HabilitadoresCreateDto dto)
    {
        var entity = new Habilitadores
        {
            Contenido = dto.Contenido ?? string.Empty,
            BasesDeDatos = dto.BasesDeDatos ?? string.Empty,
            FondosRedesFamilyOffices = dto.FondosRedesFamilyOffices ?? string.Empty,
            RedesAgremiaciones = dto.RedesAgremiaciones ?? string.Empty
        };
        await _collection.InsertOneAsync(entity);
    }

    public async Task<List<Habilitadores>> GetAllAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<Habilitadores?> GetByIdAsync(string id) =>
        await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<bool> UpdateAsync(string id, HabilitadoresCreateDto dto)
    {
        var update = Builders<Habilitadores>.Update
            .Set(x => x.Contenido, dto.Contenido)
            .Set(x => x.BasesDeDatos, dto.BasesDeDatos)
            .Set(x => x.FondosRedesFamilyOffices, dto.FondosRedesFamilyOffices)
            .Set(x => x.RedesAgremiaciones, dto.RedesAgremiaciones);

        var result = await _collection.UpdateOneAsync(x => x.Id == id, update);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }
}
