using Inteia.Api.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

    
    public class GrupoInvestigacion : BaseEntity
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Lider { get; set; }
        public bool Institucional { get; set; }
        public string ClasificacionGrupoId { get; set; }
        public string ProgramasColcienciasId { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }



