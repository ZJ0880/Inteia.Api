using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Inteia.Api.Models;
using Inteia.Api.Configurations;

public class UsuarioService
{
    private readonly IMongoCollection<Usuario> _usuarios;

    public UsuarioService(IOptions<MongoDBSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _usuarios = database.GetCollection<Usuario>(settings.Value.UsuariosCollection);
    }

    public async Task<List<Usuario>> GetAsync() =>
        await _usuarios.Find(usuario => usuario.IsActive).ToListAsync();

    public async Task<Usuario> GetByIdAsync(string id) =>
        await _usuarios.Find(usuario => usuario.Id == id && usuario.IsActive).FirstOrDefaultAsync();

    public async Task CreateAsync(Usuario usuario)
    {
        usuario.IsActive = true;
        await _usuarios.InsertOneAsync(usuario);
    }

    public async Task UpdateAsync(string id, Usuario usuarioIn) =>
        await _usuarios.ReplaceOneAsync(usuario => usuario.Id == id, usuarioIn);

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<Usuario>.Filter.Eq(u => u.Id, id);
        var update = Builders<Usuario>.Update.Set(u => u.IsActive, false);
        await _usuarios.UpdateOneAsync(filter, update);
    }
}
