// MongoRepository.cs
// ──────────────────────────────────────────────────────────
// Este repositorio genérico implementa IRepository<T>
// para trabajar con MongoDB usando MongoDB.Driver.
// T debe heredar de BaseEntity (que expone Id:string)
// ──────────────────────────────────────────────────────────
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using Inteia.Api.Configurations;

namespace Inteia.Api.Core;

public class MongoRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(IOptions<MongoDBSettings> cfg)
    {
        var client = new MongoClient(cfg.Value.ConnectionString);
        var db     = client.GetDatabase(cfg.Value.DatabaseName);

        // Colección = nombre del tipo en minúsculas p.ej. "evento"
        _collection = db.GetCollection<T>(typeof(T).Name.ToLower());
    }

    /* ------------------ Implementación que coincide 1‑a‑1 con IRepository<T> ------------------ */

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
        await _collection.Find(predicate).ToListAsync();

    public async Task<T?> GetByIdAsync(string id) =>
        await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<T> AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(T entity) =>
        await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);

    public async Task DeleteAsync(string id) =>
        await _collection.DeleteOneAsync(x => x.Id == id);
}

