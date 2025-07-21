using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Inteia.Api.Core;



namespace Inteia.Api.Core;

public class BaseEntity
{
    [BsonId]                                           // ← Mongo lo usará como _id
    [BsonRepresentation(BsonType.ObjectId)]            //  y lo convertirá a string]
    public string? Id { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
