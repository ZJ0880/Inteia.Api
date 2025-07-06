using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class UsuarioLogin
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("passwordHash")]
    public string PasswordHash { get; set; }

    [BsonElement("usuarioId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UsuarioId { get; set; }

    [BsonElement("isActive")]
    public bool IsActive { get; set; }

    [BsonElement("creationDate")]
    public DateTime CreationDate { get; set; }
}
