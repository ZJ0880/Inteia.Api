using Inteia.Api.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Inteia.Api.Models;

public enum TipoVinculador
{
    Agremiaciones,
    CamaraDeComercio
}

public class Vinculador : BaseEntity
{
    [BsonRepresentation(BsonType.String)]
    public TipoVinculador Tipo { get; set; }

    public string Nombre { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Web { get; set; } = string.Empty;
}
