using Inteia.Api.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class CamaraDeComercio : BaseEntity
{
    public string Nombre { get; set; }
    public string Municipio { get; set; }
    public string Cargo { get; set; }
    public string Lider { get; set; }
    public TipoVinculador TipoVinculador { get; set; } // Enum para relación con Vinculador
}
