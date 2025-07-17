using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using Inteia.Api.Models; 
using System.Collections.Generic;
using Inteia.Api.Configurations;

public class UsuarioLoginService
{
    private readonly IMongoCollection<UsuarioLogin> _usuariosLogin;

    public UsuarioLoginService(IOptions<MongoDBSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _usuariosLogin = database.GetCollection<UsuarioLogin>("UsuariosLogin");
    }

    public List<UsuarioLogin> Get() =>
        _usuariosLogin.Find(usuario => usuario.IsActive).ToList();

    public UsuarioLogin Get(string id) =>
        _usuariosLogin.Find(usuario => usuario.Id == id && usuario.IsActive).FirstOrDefault();

    public void Create(UsuarioLogin usuarioLogin) =>
        _usuariosLogin.InsertOne(usuarioLogin);

    public void Update(string id, UsuarioLogin usuarioIn) =>
        _usuariosLogin.ReplaceOne(usuario => usuario.Id == id, usuarioIn);

    public void Deactivate(string id)
    {
        var update = Builders<UsuarioLogin>.Update.Set(u => u.IsActive, false);
        _usuariosLogin.UpdateOne(u => u.Id == id, update);
    }
}
