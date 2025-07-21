// Models/Vinculador.cs
using Inteia.Api.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Inteia.Api.Models;

public class Vinculador : BaseEntity
{
    public string Nombre { get; set; } = default!;
    public string Ciudad { get; set; } = default!;
    public string Correo { get; set; } = default!;

    // 🔗 Relación con EntidadRelacionadora
    [BsonRepresentation(BsonType.ObjectId)]
    public string EntidadRelacionadoraId { get; set; } = default!;
}
