using Inteia.Api.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Ubicacion : BaseEntity
{
    public string Municipio { get; set; }
    public string Departamento { get; set; }
    public string Region { get; set; }
    public string Pais { get; set; }
    public string CodDane { get; set; }
}
