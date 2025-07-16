using Inteia.Api.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

    public class ClasificacionGrupo : BaseEntity
    {
        public string NombreClasificacion { get; set; }
        public string Descripcion { get; set; }
    }
