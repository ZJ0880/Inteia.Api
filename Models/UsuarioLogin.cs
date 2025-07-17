using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

public class UsuarioLogin
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("email")]
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [BsonElement("passwordHash")]
    [Required]
    public string PasswordHash { get; set; }

    [BsonElement("rol")]
    [BsonRepresentation(BsonType.String)]
    public Rol Rol { get; set; }

    [BsonElement("isActive")]
    public bool IsActive { get; set; }

    [BsonElement("creationDate")]
    public DateTime CreationDate { get; set; }

    public UsuarioLogin()
    {
        IsActive = true;
        CreationDate = DateTime.UtcNow;
        Rol = Rol.admin; 
    }
}

public enum Rol
{
    root,
    admin
}
