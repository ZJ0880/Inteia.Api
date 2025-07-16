using Inteia.Api.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


public class Evento : BaseEntity
{
     public string Nombre { get; set; }
    public string AreaConsultoria { get; set; }
    public DateTime Fecha { get; set; }
    public string Ciudad { get; set; }
    public string Organizador { get; set; }
    public string PaginaWeb { get; set; }
    public string Notas { get; set; }
}
