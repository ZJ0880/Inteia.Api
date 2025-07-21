using Inteia.Api.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ActoresCTI : BaseEntity
{
    public string InteresInteia { get; set; }
    public string NombreEntidad { get; set; }
    public string NombreActor { get; set; }
    public string ReconocidoComo { get; set; }
    public string CiudadDepartamento { get; set; }
    public string PaginaWeb { get; set; }
    public string Sector { get; set; }
    public string Resolucion { get; set; }
    public DateTime? FechaExpedicion { get; set; }
    public DateTime? FechaNotificacion { get; set; }
    public DateTime? VigenciaHasta { get; set; }
    public string GrupoInvestigacionId { get; set; } // Relación obligatoria con GrupoInvestigacion
}
