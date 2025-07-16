using Inteia.Api.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

    public class Convocatoria : BaseEntity
    {
        public string Nombre { get; set; }
        public int Año { get; set; }
    }
