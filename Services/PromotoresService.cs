using MongoDB.Driver;

public class PromotoresService
{
    private readonly IMongoCollection<Promotores> _collection;

    public PromotoresService(IMongoDatabase db)
    {
        _collection = db.GetCollection<Promotores>("Promotores");
    }

    public async Task CreateAsync(PromotoresCreateDto dto)
    {
        var entity = new Promotores
        {
            Medio = dto.Medio ?? string.Empty,
            Descripcion = dto.Descripcion ?? string.Empty,
            Direccion = dto.Direccion ?? string.Empty
        };
        await _collection.InsertOneAsync(entity);
    }

    public async Task<List<Promotores>> GetAllAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<Promotores?> GetByIdAsync(string id) =>
        await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<bool> UpdateAsync(string id, PromotoresCreateDto dto)
    {
        var update = Builders<Promotores>.Update
            .Set(x => x.Medio, dto.Medio)
            .Set(x => x.Descripcion, dto.Descripcion)
            .Set(x => x.Direccion, dto.Direccion);

        var result = await _collection.UpdateOneAsync(x => x.Id == id, update);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }
}
