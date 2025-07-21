using Inteia.Api.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public enum TipoVinculador
{
    Ninguno,
    CamaraDeComercio,
    Agremiacion
}

public class Agremiacion : BaseEntity
{
    public string Nombre { get; set; }
    public string Sigla { get; set; }
    public string RazonSocial { get; set; }
    public TipoVinculador TipoVinculador { get; set; } // Enum para relación con Vinculador
}
