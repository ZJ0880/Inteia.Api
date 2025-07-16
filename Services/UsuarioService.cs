using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Inteia.Api.Configurations;

public class UsuarioService
{
    private readonly IMongoCollection<Usuario> _usuarios;

    public UsuarioService(IOptions<MongoDBSettings> settings)
    {
        var config = settings.Value;

        if (string.IsNullOrWhiteSpace(config.UsuariosCollection))
        {
            throw new ArgumentException("El nombre de la colección de usuarios no puede ser nulo o vacío.", nameof(config.UsuariosCollection));
        }

        var client = new MongoClient(config.ConnectionString);
        var database = client.GetDatabase(config.DatabaseName);
        _usuarios = database.GetCollection<Usuario>(config.UsuariosCollection);
    }

    public List<Usuario> Get() =>
        _usuarios.Find(usuario => true).ToList();

    public Usuario Get(string id) =>
        _usuarios.Find(usuario => usuario.Id == id).FirstOrDefault();

    public void Update(string id, Usuario usuarioIn) =>
        _usuarios.ReplaceOne(usuario => usuario.Id == id, usuarioIn);

    public void Delete(string id) =>
        _usuarios.DeleteOne(usuario => usuario.Id == id);

    public void Deactivate(string id)
    {
        var update = Builders<Usuario>.Update.Set(u => u.IsActive, false);
        _usuarios.UpdateOne(u => u.Id == id, update);
    }
}
